using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJSheng.App
{
    public class Test
    {
        private List<Goods> goodList;
        private int _totalprice;
        public Test(int totalprice)
        {
            //以下是自测试随机生成商品和价格列表，实际应用中可以用select语句获取所有单价小于totalprice的商品列表            
            goodList = new List<Goods>();
            for (int i = 0; i < 10; i++)
            {
                goodList.Add(new Goods()
                {
                    Name = string.Format("第{0}个商品", (i + 1).ToString()),
                    Price = (i + 1) * 3
                });
            }
            _totalprice = totalprice;
        }
        public List<List<Goods>> GetAllSelection()
        {
            List<Goods> nowgoods = new List<Goods>();
            List<List<Goods>> result = getSel(_totalprice, nowgoods);
            return result;
        }

        private List<List<Goods>> getSel(int total, List<Goods> nowgoods)
        {
            List<List<Goods>> goods = new List<List<Goods>>();
            for (int i = 0; i < goodList.Count; i++)
            {
                if (goodList[i].Price == total)
                {
                    goods.Add(new List<Goods>());
                    goods[goods.Count - 1].AddRange(nowgoods);
                    goods[goods.Count - 1].Add(goodList[i]);
                }
                else if (goodList[i].Price < total)
                {
                    nowgoods.Add(goodList[i]);
                    goods.AddRange(getSel(total - goodList[i].Price, nowgoods));
                    nowgoods.RemoveAt(nowgoods.Count - 1);
                }
            }
            return goods;
        }
    }

}
