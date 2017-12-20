using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.Domain;

namespace WellApp.Repo.Text
{
    public class TextLoader
    {
        // TODO implement abstract class
        string _path;       

        public TextLoader(string path)
        {
            _path = path;
        }
        public void LoadText(params ITextReport[] reports)
        {
            if (File.Exists(_path))
            {              
                using (var sr = new StreamReader(_path))
                {
                    sr.ReadLine(); // skip header line   
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var elems = line.Split('|');    
                        foreach (var rep in reports)
                        {
                            if (rep.Validate(elems))
                            {
                                rep.Map(elems); 
                            }
                        }
                    } 
                }
            }
            
        }
    }
}
