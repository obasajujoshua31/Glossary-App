using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;



namespace Glossary.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class GlossaryController : ControllerBase
    {
        private static List<GlossaryItem> Glossary = new List<GlossaryItem>
        {
            new GlossaryItem
            {
                Term = "Access Token",
                Definition = "A credential that can be used by an application to access an API. It informs the API"
            },

            new GlossaryItem
            {
                Term= "JWT",
                Definition = "An open, industry standard RFC 7519 method for representing claims securely between two parties. "
            },
            new GlossaryItem
            {
                Term= "OpenID",
                Definition = "An open standard for authentication that allows applications to verify users are who they say they are without needing to collect, store, and therefore become liable for a user’s login information."
            },

              new GlossaryItem
            {
                Term= "Validation",
                Definition = "An open standard for authentication that allows applications to verify users are who they say they are without needing to collect, store, and therefore become liable for a user’s login information."
            }
        };

        [HttpGet]
        public ActionResult<List<GlossaryItem>> Get()
        {
            return Ok(Glossary);
        }


        [HttpGet]
        [Route("{term}")]
        public ActionResult<GlossaryItem> Get(string term)
        {
            var glossaryItem = Glossary.Find(item => item.Term.Equals(term, StringComparison.InvariantCultureIgnoreCase));

            if (glossaryItem == null)
            {
                return NotFound();
            }

            return Ok(glossaryItem);
        }


        [HttpPost]
        public ActionResult Post(GlossaryItem glossaryItem)
        {
            var existingGlossaryItem = Glossary.Find(item =>
            item.Term.Equals(glossaryItem.Term, StringComparison.InvariantCultureIgnoreCase));

            if (existingGlossaryItem != null )
            {
                return Conflict("Cannot create the term because it already exists");
            }

            Glossary.Add(glossaryItem);

            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeUriString(glossaryItem.Term));

            return Created(resourceUrl, glossaryItem);
        }



        [HttpPut]
        [Route("{term}")]
        public ActionResult<GlossaryItem> Put(string term, GlossaryItem glossaryItem)
        {
            var existingGlossaryItem = Glossary.Find(item =>
                item.Term.Equals(term, StringComparison.InvariantCultureIgnoreCase)
            );

            if (existingGlossaryItem == null )
            {
                return BadRequest("Cannot update a non existant term");
            }

            existingGlossaryItem.Definition = glossaryItem.Definition;

            return Ok(existingGlossaryItem);
        }


        [HttpDelete]
        [Route("{term}")]
        public ActionResult Delete(string term)
        {
            var existingGlossaryItem = Glossary.Find(item =>
            item.Term.Equals(term, StringComparison.InvariantCultureIgnoreCase));

            if (existingGlossaryItem == null)
            {
                return NotFound();
            }

            Glossary.Remove(existingGlossaryItem);
            return NoContent();
        }
     }
}
