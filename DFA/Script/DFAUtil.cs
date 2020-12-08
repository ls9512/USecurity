/////////////////////////////////////////////////////////////////////////////
//
//  Script   : DFAUtil.cs
//  Info     : 敏感词过滤辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace Aya.Security
{
    public class DFAUtil
    {
        public static bool IsInit { get; private set; }
        public static string ReplaceSymbol = "*";
        public static string[] Separator = new[] { ",", "\n", "\r" };

        private static readonly DFANode RootDfaNode = new DFANode('R');
        private static readonly List<string> Word = new List<string>();

        /// <summary>
        /// 初始化敏感字过滤
        /// </summary>
        /// <param name="dfaText">敏感词数据文件</param>
        public static void Init(string dfaText)
        {
            if (IsInit) return;
            CreateTree(dfaText.Split(Separator, StringSplitOptions.RemoveEmptyEntries));
            IsInit = true;
        }

        /// <summary>
        /// 过滤词汇
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="isLimit">是否被过滤</param>
        /// <returns>输出</returns>
        public static string FilterWords(string input, out bool isLimit)
        {
            isLimit = false;
            if (!IsInit)
            {
                return input;
            }

            var index = 0;
            Word.Clear();
            var chArray = input.ToCharArray();
            var rootNode = RootDfaNode;
            var builder = new StringBuilder();
            while (index < chArray.Length)
            {
                var c = chArray[index];
                if (char.IsUpper(c))
                {
                    c = c.ToString().ToLower()[0];
                }

                rootNode = FindNode(rootNode, c);
                if (rootNode == null)
                {
                    rootNode = RootDfaNode;
                    Word.Clear();
                    rootNode = FindNode(rootNode, c);
                    if (rootNode == null)
                    {
                        builder.Append(chArray[index]);
                        rootNode = RootDfaNode;
                    }
                    else
                    {
                        Word.Add(chArray[0].ToString());
                        builder.Append(chArray[index]);
                    }
                }
                else if (rootNode.Flag == 1)
                {
                    Word.Add(chArray[index].ToString());
                    builder.Append(chArray[index]);
                    var builder2 = new StringBuilder();
                    builder2.Append(ReplaceSymbol);
                    builder.Remove(builder.Length - Word.Count, Word.Count);
                    builder.Append(builder2);
                    Word.Clear();
                    rootNode = RootDfaNode;
                    isLimit = true;
                }
                else
                {
                    Word.Add(chArray[index].ToString());
                    builder.Append(chArray[index]);
                }

                index++;
            }

            return builder.ToString();
        }

        #region Private

        /// <summary>
        /// 创建查找树
        /// </summary>
        /// <param name="arr">字符串数组</param>
        private static void CreateTree(string[] arr)
        {
            for (var i = 0; i < arr.Length; i++)
            {
                var str = arr[i];
                var cs = str.Trim().ToLower().ToCharArray();
                if (cs.Length > 0)
                {
                    InsertNode(RootDfaNode, cs, 0);
                }
            }
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="dfaNode">父节点</param>
        /// <param name="c">字符</param>
        /// <returns>结果</returns>
        private static DFANode FindNode(DFANode dfaNode, char c)
        {
            var nodes = dfaNode.Nodes;
            for (var i = 0; i < nodes.Count; i++)
            {
                var n = nodes[i];
                if (n.Char == c)
                {
                    return n;
                }
            }

            return null;
        }

        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="dfaNode">父节点</param>
        /// <param name="cs">字符数组</param>
        /// <param name="index">索引</param>
        private static void InsertNode(DFANode dfaNode, char[] cs, int index)
        {
            var item = FindNode(dfaNode, cs[index]);
            if (item == null)
            {
                item = new DFANode(cs[index]);
                dfaNode.Nodes.Add(item);
            }

            if (index == (cs.Length - 1))
            {
                item.Flag = 1;
            }

            index++;
            if (index < cs.Length)
            {
                InsertNode(item, cs, index);
            }
        }

        #endregion
    }
}