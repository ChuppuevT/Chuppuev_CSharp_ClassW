using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using MigraDoc.DocumentObjectModel.Tables;
using System.Text;

namespace AbstractFoodOrderBusinessLogic.HelperModels
{
    class PdfRowParameters
    {
        public Table Table { get; set; }
        public List<string> Texts { get; set; }
        public string Style { get; set; }
        public ParagraphAlignment ParagraphAlignment { get; set; }
    }
}
