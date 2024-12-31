using System.Collections.Generic;

namespace ActivoFijoAPI.Util
{

  public class Pager
  {
    public int Page { get; set; }
    public int Fetch { get; set; }
    public int Total { get; set; }

    

    public Pager(int page, int fetch, int total)
    {
      this.Page = page;
      this.Fetch = fetch;
      this.Total = total;
    }
  }


  public class DataTableView<T>
  {
    public Pager Pager { get; set; }

    public List<T> Results { get; set; }

    public DataTableView(Pager pager, List<T> list)
    {
      this.Pager = pager;
      this.Results = list;
    }
  }


}



 