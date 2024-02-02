using Dapper;
using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.AutoMapper;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models;
using System.Data;
using System.Globalization;
using System.Xml;

namespace SistemaGestaoVendas.Controllers
{
    public class ConciliacaoContasController : Controller
    {
        private readonly IContasAPagar _contasAPagarRepository;
        private readonly IContasAReceber _contasAReceberRepository;
        private readonly IConciliacaoContas _conciliacaoContasRepository;
        private readonly Dao _dao;


        public ConciliacaoContasController(IContasAPagar contasAPagar, IContasAReceber contasAReceber, IConciliacaoContas conciliacaoContasRepository, Dao dao)
        {
            _contasAPagarRepository = contasAPagar;
            _contasAReceberRepository = contasAReceber;
            _conciliacaoContasRepository = conciliacaoContasRepository ?? throw new ArgumentNullException(nameof(conciliacaoContasRepository));
            _dao = dao;
        }

        public static List<OfxTransaction> ParseOfxContent(string ofxContent, IConciliacaoContas conciliacaoContasRepository)
        {
            List<OfxTransaction> transactions = new List<OfxTransaction>();

               // Encontre a posição inicial da tag <OFX>
                int startIndex = ofxContent.IndexOf("<OFX>");
                if (startIndex >= 0)
                {
                    // Substitua o conteúdo OFX para começar a partir da tag <OFX>
                    ofxContent = ofxContent.Substring(startIndex);
                }
                else
                {
                    Console.WriteLine("Conteúdo OFX inválido: a tag <OFX> não foi encontrada.");
                    return transactions;
                }

                string[] tagsToClose = { "TRNUID", "CURDEF", "BANKID", "ACCTID", "DTSTART", "DTEND", "TRNTYPE", "DTPOSTED", "TRNAMT", "FITID", "CHECKNUM", "MEMO", "BALAMT", "DTASOF", "MKTGINFO" };
                ofxContent = CloseOpenTags(ofxContent, tagsToClose);

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ofxContent);

                XmlNodeList transactionNodes = doc.SelectNodes("//STMTTRN");
                foreach (XmlNode node in transactionNodes)
                {
                    OfxTransaction? transaction = new OfxTransaction();

                    transaction.DATA = node.SelectSingleNode("DTPOSTED").InnerText;
                    transaction.TRANSACTIONTYPE = node.SelectSingleNode("TRNTYPE").InnerText;
                    transaction.TRNAMT = node.SelectSingleNode("TRNAMT").InnerText;
                    transaction.FITID = node.SelectSingleNode("FITID").InnerText;
                    transaction.CHECKNUM = node.SelectSingleNode("CHECKNUM").InnerText;
                    transaction.MEMO = node.SelectSingleNode("MEMO").InnerText;
                    
                    transactions.Add(transaction);
                    conciliacaoContasRepository.Insert(transaction);
                }
            }
            catch (XmlException ex)
            {
                Console.WriteLine("Erro ao analisar o conteúdo OFX: " + ex.Message);
            }

            return transactions;
        }

        [HttpPost]
        public IActionResult ConciliacaoContas(IFormFile arquivoExtrato)
        {
            // Verifica se o arquivo foi enviado
            if (arquivoExtrato == null || arquivoExtrato.Length == 0)
            {
                ModelState.AddModelError("arquivoExtrato", "Por favor, selecione um arquivo para importar.");
                return View("ConciliacaoContas");
            }

            using (var reader = new StreamReader(arquivoExtrato.OpenReadStream()))
            {
                var ofxContent = reader.ReadToEnd();
                var transactions = ParseOfxContent(ofxContent, _conciliacaoContasRepository);
                return RedirectToAction("ConciliacaoContas", "ConciliacaoContas");
            }
        }

        public IActionResult ConciliacaoContas()
        {
            var contasAPagar = _contasAPagarRepository.GetAll();
            var contasAReceber = _contasAReceberRepository.GetAll();
            var ofx = _conciliacaoContasRepository.GetAll();
            
            var viewModel = new ConciliacaoViewModel
            {
                ContasAPagar = contasAPagar,
                ContasAReceber = contasAReceber,
                Ofx = ofx
            };

            return View(viewModel);
        }
        
        private static string CloseOpenTags(string content, string[] tagNames)
        {
            foreach (string tagName in tagNames)
            {
                string openTag = "<" + tagName + ">";
                string closeTag = "</" + tagName + ">";

                // Encontra todas as ocorrências da tag de abertura
                int startIndex = content.IndexOf(openTag);
                while (startIndex != -1)
                {
                    // Encontra o índice de fechamento da tag de abertura
                    int endIndex = content.IndexOf('>', startIndex);

                    // Encontra a próxima tag de abertura
                    int nextOpenIndex = content.IndexOf("<", endIndex);

                    // Encontra a próxima tag de fechamento
                    int nextCloseIndex = content.IndexOf(closeTag, endIndex);

                    // Verifica se a próxima tag de fechamento está ausente ou se está antes da próxima tag de abertura
                    if (nextCloseIndex == -1 || (nextOpenIndex != -1 && nextOpenIndex < nextCloseIndex))
                    {
                        // Pega o conteúdo entre as tags de abertura e fechamento
                        string tagContent = content.Substring(endIndex + 1, nextOpenIndex - endIndex - 1);

                        // Insere a tag de fechamento logo após o conteúdo da tag de abertura
                        content = content.Insert(endIndex + 1 + tagContent.Length, closeTag);
                    }

                    // Encontra a próxima ocorrência da tag de abertura
                    startIndex = content.IndexOf(openTag, startIndex + 1);
                }
            }
            return content;
        }
    }
}
