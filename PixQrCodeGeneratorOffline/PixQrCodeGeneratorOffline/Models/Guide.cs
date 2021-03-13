using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models
{
    public class Guide
    {
        public Guide(string _question, string _answer)
        {
            Question = _question;
            Answer = _answer;
        }

        public string Question { get; set; }

        public string Answer { get; set; }
    }
}
