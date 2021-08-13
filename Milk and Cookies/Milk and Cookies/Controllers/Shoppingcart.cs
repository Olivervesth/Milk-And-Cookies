using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Collections.Generic;

namespace sessions_og_cookies.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Shoppingcart : ControllerBase
    {

        [HttpGet]
        [Route("{id}/{productname}/{price}")]
        public string AddProduct(int id, string productname, int price)
        {
            Product newp = new Product();//Making new product object and fills it with the parameters
            newp.id = id;
            newp.name = productname;
            newp.price = price;
            List<Product> plist = new List<Product>();

            plist.Add(newp);
            string jsonstring = "";
            string cart = HttpContext.Session.GetString("shoppingcart");//lets get the data thats in the session
            if (cart != null)
            {
                List<Product> cartlist = JsonSerializer.Deserialize<List<Product>>(cart);//We are Deserializing the jsonstring we got from session transforming it into a list of products
                plist.AddRange(cartlist);
                jsonstring = JsonSerializer.Serialize(plist);
            }
            else
            {
                jsonstring = JsonSerializer.Serialize(plist);
            }

            Request.HttpContext.Session.SetString("shoppingcart", jsonstring);//Set new data in session
            return "saved";
        }

        [HttpGet]
        [Route("shoppingcart")]
        public string GetProducts()
        {
            string cartstring = HttpContext.Session.GetString("shoppingcart");
            if (cartstring != null)
            {
                return cartstring;//Return the data in session
            }
            else
            {
                return "Your kurv is empty!";
            }


        }

        [HttpGet]
        [Route("delete/{id}")]
        public string DeleteProduct(int id)
        {
            List<Product> cartlist = JsonSerializer.Deserialize<List<Product>>(HttpContext.Session.GetString("shoppingcart"));

            for (int i = 0; i < cartlist.Count; i++)//Compares all the ids in the cart with the one you wanna delete
            {
                if (cartlist[i].id == id)
                {
                    string delitem = cartlist[i].name;
                    cartlist.Remove(cartlist[i]);//remove item from cart
                    Request.HttpContext.Session.SetString("shoppingcart", JsonSerializer.Serialize(cartlist));//Send back the cart with the removed item
                    return "Deleted :" + delitem;

                }
            }

            return "No item with that id in your cart!";

        }
    }
}
