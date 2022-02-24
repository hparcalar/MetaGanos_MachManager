using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MachManager.Context;

namespace MachManager.i18n{
    public class Translation{
        MetaGanosSchema _context;
        public Translation(MetaGanosSchema context){
            _context = context;

            FillLangBooks();
        }

        private void FillLangBooks(){
            try
            {
                this.LangBooks.Clear();

                // add default language & responses
                var defBook = new LangBook("default");
                var defExpValues = Enum.GetValues(typeof(Expressions));
                foreach (var defExp in defExpValues)
                {
                    if (DefaultEqualResponses.List.ContainsKey((Expressions)defExp))
                        defBook.AddExpression((Expressions)defExp, DefaultEqualResponses.List[(Expressions)defExp]);
                }
                this.LangBooks.Add(defBook);

                // add user defined responses and languages
                var langList = _context.SysLang.ToArray();
                foreach (var lang in langList)
                {
                    var newBook = new LangBook(lang.LanguageCode);

                    var expList = _context.SysLangDict
                        .Where(d => d.SysLangId == lang.Id).ToArray();
                    
                    foreach (var exp in expList)
                    {
                        if (exp.ExpNo != null)
                            newBook.AddExpression((Expressions)exp.ExpNo, exp.EqualResponse);
                    }

                    this.LangBooks.Add(newBook);
                }
            }
            catch (System.Exception)
            {
                
            }
        }

        private IList<LangBook> LangBooks = new List<LangBook>();
        
        public string Translate(Expressions expression, string languageCode){
            string equalResponse = string.Empty;

            // search related book
            var relatedBook = this.LangBooks.FirstOrDefault(d => d.LanguageCode == languageCode);
            if (relatedBook != null){
                equalResponse = relatedBook.GetExpression(expression);
            }

            // if not found then look for default response
            if (string.IsNullOrEmpty(equalResponse)){
                var defBook = this.LangBooks.FirstOrDefault(d => d.LanguageCode == "default");
                if (defBook != null){
                    equalResponse = defBook.GetExpression(expression);
                }
            }

            return equalResponse;
        }
    }
}