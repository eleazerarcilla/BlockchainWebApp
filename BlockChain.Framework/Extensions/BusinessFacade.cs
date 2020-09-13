using BlockChain.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using static BlockChain.Framework.Extensions.GlobalExtensions;

namespace BlockChain.Framework.Extensions
{
    public class BusinessFacade
    {
        public void ProcessUploadedFile(List<ProductList> productList, List<CommonItems> commonItems, List<PermittedAdditive> permittedAdditives)
        {
            foreach(ProductList product in productList)
            {
                string[] ingredients = Regex.Split(product.TX_INGREDIENTS.Replace("(", "'").Replace(")", "'"), ",(?=(?:[^']*'[^']*')*[^']*$)");
                List<string> prohibitedIngredients = new List<string>();
                bool hasProhibited = false;
                foreach (string ing in ingredients)
                {
                    if (permittedAdditives
                        .Select(x => x)
                        .Where(x => x.TX_ADDITIVE_NAME.RemoveSpaces().Equals(ing.RemoveSpaces(), StringComparison.OrdinalIgnoreCase)
                        || x.TX_ADDITIVE_NAME_FR.RemoveSpaces().Equals(ing.RemoveSpaces(), StringComparison.OrdinalIgnoreCase)
                        || x.TX_ADDITIVE_NAME.RemoveSpaces().Split(',').Contains(ing.RemoveSpaces())
                        || x.TX_ADDITIVE_NAME_FR.RemoveSpaces().Split(',').Contains(ing.RemoveSpaces())).ToList().Count == 0)
                    {
                        if (commonItems
                            .Select(x=>x)
                            .Where(x=>x.TX_COMMON_ITEM.RemoveSpaces().Equals(ing.RemoveSpaces(), StringComparison.OrdinalIgnoreCase)).ToList().Count == 0)
                        {
                            hasProhibited = true;
                            int firstPipe = ing.IndexOf('\'');
                            int secondPipe = ing.LastIndexOf('\'');
                            StringBuilder sb = new StringBuilder(ing);
                            if (firstPipe > -1)
                                sb[firstPipe] = '(';
                            if (secondPipe > -1)
                                sb[secondPipe] = ')';
                            string prohibited_ing = sb.ToString();
                            prohibitedIngredients.Add(prohibited_ing);
                        }
                    }
                    if (hasProhibited)
                    {
                        product.HAS_PROHIBITED = hasProhibited;
                        product.TX_PROHIBITED_INGREDIENTS = string.Join(", ", prohibitedIngredients);
                    }
                }
            }
            //Regex.Split("1,2,3, '1,2,3'", ",(?=(?:[^']*'[^']*')*[^']*$)");
        }
    }
}
