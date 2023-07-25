using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_core_MVC.Models
{
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public decimal Revene { get; set; }

    [DataType(DataType.Date)]
    public DateTime ReleseDate { get; set; }
}
public class Movie
{
    public int id { get; set; }

}

