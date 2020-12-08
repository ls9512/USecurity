/////////////////////////////////////////////////////////////////////////////
//
//  Script   : DFANode.cs
//  Info     : 敏感词过滤 -- 查找树节点
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;

namespace Aya.Security
{
	internal class DFANode
	{
		public char Char;
		public int Flag;
		public List<DFANode> Nodes;

		public DFANode(char c)
		{
			Nodes = new List<DFANode>();
			Char = c;
			Flag = 0;
		}

		public DFANode(char c, int flag)
		{
			Nodes = new List<DFANode>();
			Char = c;
			Flag = flag;
		}
	}
}