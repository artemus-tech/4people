using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4People.StatisticalTestTask.Model
{
    class Tree
    {
        public class TreeNode
        {
            public int Value; // численное значение
            public int Count = 0; // сколько раз было добавлено данное численное значение
            public TreeNode Left; // левое поддерево
            public TreeNode Right; // правое поддерево
        }
        public TreeNode Node; // экземпляр класса "элемент дерева"
    }
}
