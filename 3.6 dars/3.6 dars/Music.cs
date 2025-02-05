using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._6_dars;

public class Music
{ //[JsonPropertyName("id")]
    public Guid? Id { get; set; }

    //[JsonPropertyName("name")]
    public string Name { get; set; }

    //[JsonPropertyName("mb")]
    public double MB { get; set; }

    //[JsonPropertyName("authorName")]
    public string AuthorName { get; set; }

    //[JsonPropertyName("description")]
    public string Description { get; set; }

    //[JsonPropertyName("quentityLikes")]
    public int QuentityLikes { get; set; }

    public override string ToString()
    {
        return $"Id : {Id}, Name : {Name}, Mb : {MB}, AName : {AuthorName}, Des : {Description}, QL : {QuentityLikes}";
    }
}
