using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema
{
    public static class Language
    {
        public enum Lang{
            En,
            Ru,
            Zh
        }
        public static Lang currentLanguage = Lang.En;
    }

}