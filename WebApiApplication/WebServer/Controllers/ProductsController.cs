using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers {

    [Route("api/[controller]")]
    public class ProductsController : Controller {

        [HttpGet]

        //kanoume elegxo an den einai kenh h lista epestrepse ta apotelesmata se enan array
        public ActionResult Get() {
            if(FakeData.Products != null)
            {
                return Ok(FakeData.Products.Values.ToArray());
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpGet("{id}")]

        //an yparxei to key tote tha dwsei to value ,alliws tha vgei mhnyma notfound
        public ActionResult Get(int id) {
            if (FakeData.Products.ContainsKey(id))
            {
                return Ok(FakeData.Products[id]);
            }  
            else
            {
                return NotFound();
            }

        }

        [HttpGet("from/{low}/to/{high}")]

        //arxika tha paroume ta products poy einai metaksi ths timhs low kai high
        //kai sthn synexeia tha ta vazei se enan array
        public ActionResult Get(int low, int high) {
            var products = FakeData.Products.Values
            .Where(p => p.Price >= low && p.Price <= high).ToArray();
            //tha eleksw an o array einai  h den einai adeios
            //anexei akti mesa tha to emfamisei alliws mhnyma oti dne vrethike
            if(products.Length > 0) 
            {
                return Ok(products);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        //me thn methodo post  to id tha parei thn megisth timh twn hdh yparxontwn ayksimeno kata 1
        //sthn synexeia tha to valeis thn lista to id mazi me to object
        public ActionResult Post([FromBody]Product product) {
            product.ID = FakeData.Products.Keys.Max() + 1;
            FakeData.Products.Add(product.ID, product);
            //kai epistrefei mhnyma oti dhmioygrithike
            //mesa sto created pername to url tou neodhmioyrghmenou proiontos ,ayto to url  tha einai mesa ston htttp response header
            return Created($"api/products/{product.ID}",product);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Product product) {
            
            //ean yparxei to key  tote to object pou pername 
            if (FakeData.Products.ContainsKey(id)) 
            {
                
                var target = FakeData.Products[id];
                //tha ginoyn initialize ta properties tou
                target.ID = product.ID;
                target.Name = product.Name;
                target.Price = product.Price;
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) {
            if (FakeData.Products.ContainsKey(id)) 
            {
                //an diagrafei epistrefei mhnyma epityxias
                FakeData.Products.Remove(id);
                return Ok();
            }
            else
            {
                //alliws oti den vrethike
                return NotFound();
            }
        }
    }
}