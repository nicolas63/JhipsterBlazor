using System;
using System.Collections.Generic;
using System.Text;
using SharedModel.Constants;

namespace SharedModel.Models
{
    public class JhiAlert
    {
        public int Id { get; set; }
        public TypeAlert Type { get; set; }
        public string Msg { get; set; }
        public string Params { get; set; }
        public int Timeout { get; set; }
        public bool Toast { get; set; }
        public string Position { get; set; }
        public bool Scoped { get; set; }
    }
}
