using System;
using System.Collections;
using System.Collections.Generic;

namespace MachManager.i18n{
    public class LangBook{
        public string LanguageCode { get; set; }

        public LangBook(string languageCode){
            this.LanguageCode = languageCode;
        }

        private Dictionary<Expressions, string> _book = null;
        protected Dictionary<Expressions, string> Book 
        { 
            get{
                if (_book == null)
                    _book = new Dictionary<Expressions, string>();

                return _book;
            }
        }

        public string GetExpression(Expressions expression){
            return this.Book.ContainsKey(expression) ?
                this.Book[expression] : string.Empty;
        }

        public void AddExpression(Expressions expressions, string translation){
            if (this.Book.ContainsKey(expressions))
                this.Book[expressions] = translation;
            else
                this.Book.Add(expressions, translation);
        }

        public void Clear(){
            this.Book.Clear();
        }
    }
}