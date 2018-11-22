using Microsoft.AspNetCore.Mvc;
using WebServer.Models;
using System.Linq;

namespace WebServer.Controllers
{

    //κανω  attibute to route
    [Route("api/[controller]")]

    public class ProductsController : Controller
    {

        [HttpGet]
        public Product[] Get()
        {
            //me thn methodo get pernw tis times tis listas kai tis topothetw se array
            return FakeData.Products.Values.ToArray();
        }

        //edw twra tha kanoume get me ena sygkekrimeno id 
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            //elegxei an yparxei to sygkekrimeno id sta kleidia tou dictionary
            if(FakeData.Products.ContainsKey(id))
            {
                return FakeData.Products[id];
                
            }
            else
            {
                return null;
            }

        }

        //vazoume action gia to post 
        [HttpPost]
        public Product Post([FromBody] Product product)
        {
            //sto kainourio id pernei to megisto kleidi apo to dic kai to ayksanei kata ena
            product.ID = FakeData.Products.Keys.Max()+1;
            //kai sthn synexeia vazoume mesa sto dictionary to neo kleidi kai value opoy einai ena object typou product
            FakeData.Products.Add(product.ID,product);

            return product;
        }

        //ftiaxnoume to action put gia na kanoume update ean to product den yparxei den kanei tipota
        [HttpPut("{id}")]

        public void Put(int id,[FromBody] Product product)
        {
            //elegxoume oti yparxei to key 
            if (FakeData.Products.ContainsKey(id))
            {
                //pernw thn value vazwntas mesa se agkiles sto dict thn timh tou key 
                var target = FakeData.Products[id];
                //to value ayto einai ena object kai tou vazw ta properties ti times na pernei
                target.ID=product.ID;
                target.Name = product.Name;
                target.Price = product.Price;
            }

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //elegxei an yparxei to kleidi me to sygkekrimeno id
            if(FakeData.Products.ContainsKey(id))
            {
                //tote to svhnei
                FakeData.Products.Remove(id);
            }
        }


    }
}