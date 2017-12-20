using System.Collections.Generic;

namespace WellApp.Repo.Text
{
    public interface ITextReport
    {
        void Map(string[] line);
        bool Validate(string[] line);
    }
}