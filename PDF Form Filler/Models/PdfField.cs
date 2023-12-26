using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_Form_Filler.Models
{
    public class PdfField
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public PdfFieldAttributes Attributes { get; set; }
    }
}
