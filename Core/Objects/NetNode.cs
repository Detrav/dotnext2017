using System;
using System.Collections.Generic;
using System.Text;

namespace Objects
{
    class NetNode
    {
        public NetNode FirstChild { get; set; }
        public NetNode SecondChild { get; set; }
        public NetNode ThirdChild { get; set; }
        public NetNode FourthChild { get; set; }
        public NetNode FifthChild { get; set; }
        public NetNode FirstParent { get; set; }
        public NetNode SecondParent { get; set; }
        public NetNode ThirdParent { get; set; }
        public NetNode FourthParent { get; set; }
        public NetNode FifthParent { get; set; }
        public NetNode FirstNeighbor { get; set; }
        public NetNode SecondNeighbor { get; set; }
        public NetNode ThirdNeighbor { get; set; }
        public NetNode FourthNeighbor { get; set; }

        public object[] Array { get; set; }
        public List<object> List { get; set; }
        public Dictionary<string, object> Dictionary { get; set; }
        public StringBuilder Builder { get; set; }
        public string String { get; set; }

        public NetNode(bool fullInit)
        {
            if (fullInit)
            {
                Array = new object[0];
                List = new List<object>();
                Dictionary = new Dictionary<string, object>();
                Builder = new StringBuilder();
                String = "b165579034d5538683ed211ede1e9d64f68143eb59258763ff11d7a908775a76e86eb7155379fb568efab478ce0cb65a2c7f1787dbcaf1a421f7bca91d2c75aedd72455067c08d9b4b997de52de34b071dc1a70cce130914378bc070f10671ba74fa080cbdd8ca48ca0d34b06b18794a37cba63a6b0e3e44c7ad0e628f4468528255e8e565684dc77dcfe794093b2e4e6d608f63956a7eeb2cd9b5308145532718267cf878680ec2b8851a20a577a1e62f9fc746d423fa0942adad5651c247d6a0615d63328d8f1f4e3860c592aab2257c9f4e46c69758e60cd545f930833b81025c61a6acc074f0349e4c02cfb46030d762310e16b88e2f08fe29c413f66609";
            }
        }

        public NetNode()
        {
            Array = new object[0];
            List = new List<object>();
            Dictionary = new Dictionary<string, object>();
            Builder = new StringBuilder();
            String = "b165579034d5538683ed211ede1e9d64f68143eb59258763ff11d7a908775a76e86eb7155379fb568efab478ce0cb65a2c7f1787dbcaf1a421f7bca91d2c75aedd72455067c08d9b4b997de52de34b071dc1a70cce130914378bc070f10671ba74fa080cbdd8ca48ca0d34b06b18794a37cba63a6b0e3e44c7ad0e628f4468528255e8e565684dc77dcfe794093b2e4e6d608f63956a7eeb2cd9b5308145532718267cf878680ec2b8851a20a577a1e62f9fc746d423fa0942adad5651c247d6a0615d63328d8f1f4e3860c592aab2257c9f4e46c69758e60cd545f930833b81025c61a6acc074f0349e4c02cfb46030d762310e16b88e2f08fe29c413f66609";
        }

        public void AssignChild(NetNode fisrtChild,NetNode secondChild, NetNode thirdChild, NetNode fourthChild, NetNode fifthChild)
        {
            FirstChild = fisrtChild;
            SecondChild = secondChild;
            ThirdChild = thirdChild;
            FourthChild = fourthChild;
            FifthChild = fifthChild;
        }

        public void AssignParent(NetNode fisrtParent, NetNode secondParent, NetNode thirdParent, NetNode fourthParent, NetNode fifthParent)
        {
            FirstParent = fisrtParent;
            SecondParent = secondParent;
            ThirdParent = thirdParent;
            FourthParent = fourthParent;
            FifthParent = fifthParent;
        }

        public void AssignNeighbor(NetNode fisrtNeighbor, NetNode secondNeighbor, NetNode thirdNeighbor, NetNode fourthNeighbor)
        {
            FirstNeighbor = fisrtNeighbor;
            SecondNeighbor = secondNeighbor;
            ThirdNeighbor = thirdNeighbor;
            FourthNeighbor = fourthNeighbor;
        }

        public void Assign(NetNode reference)
        {
            FirstParent = reference.FirstParent;
            SecondParent = reference.SecondParent;
            ThirdParent = reference.ThirdParent;
            FourthParent = reference.FourthParent;
            FifthParent = reference.FifthParent;

            FirstChild = reference.FirstChild;
            SecondChild = reference.SecondChild;
            ThirdChild = reference.ThirdChild;
            FourthChild = reference.FourthChild;
            FifthChild = reference.FifthChild;

            FirstNeighbor = reference.FirstNeighbor;
            SecondNeighbor = reference.SecondNeighbor;
            ThirdNeighbor = reference.ThirdNeighbor;
            FourthNeighbor = reference.FourthNeighbor;

            FirstParent?.ReplaceChild(reference, this);
            SecondParent?.ReplaceChild(reference, this);
            ThirdParent?.ReplaceChild(reference, this);
            FourthParent?.ReplaceChild(reference, this);
            FifthParent?.ReplaceChild(reference, this);

            FirstChild?.ReplaceParent(reference, this);
            SecondChild?.ReplaceParent(reference, this);
            ThirdChild?.ReplaceParent(reference, this);
            FourthChild?.ReplaceParent(reference, this);
            FifthChild?.ReplaceParent(reference, this);

            FirstNeighbor.ReplaceNeighbor(reference, this);
            SecondNeighbor.ReplaceNeighbor(reference, this);
            ThirdNeighbor.ReplaceNeighbor(reference, this);
            FourthNeighbor.ReplaceNeighbor(reference, this);
        }

        public void ReplaceChild(NetNode from, NetNode to)
        {
            if (FirstChild == from)
                FirstChild = to;
            else if (SecondChild == from)
                SecondChild = to;
            else if (ThirdChild == from)
                ThirdChild = to;
            else if (FourthChild == from)
                FourthChild = to;
            else if (FifthChild == from)
                FifthChild = to;
        }


        public void ReplaceNeighbor(NetNode from, NetNode to)
        {
            if (FirstNeighbor == from)
                FirstNeighbor = to;
            else if (SecondNeighbor == from)
                SecondNeighbor = to;
            else if (ThirdNeighbor == from)
                ThirdNeighbor = to;
            else if (FourthNeighbor == from)
                FourthNeighbor = to;
        }


        public void ReplaceParent(NetNode from, NetNode to)
        {
            if (FirstParent == from)
                FirstParent = to;
            else if (SecondParent == from)
                SecondParent = to;
            else if (ThirdParent == from)
                ThirdParent = to;
            else if (FourthParent == from)
                FourthParent = to;
            else if (FifthParent == from)
                FifthParent = to;
        }

        public void RemoveRaw()
        {
            FirstParent?.AssignChild(FirstChild, SecondChild, ThirdChild, FourthChild, FifthChild);
            SecondParent?.AssignChild(FirstChild, SecondChild, ThirdChild, FourthChild, FifthChild);
            ThirdParent?.AssignChild(FirstChild, SecondChild, ThirdChild, FourthChild, FifthChild);
            FourthParent?.AssignChild(FirstChild, SecondChild, ThirdChild, FourthChild, FifthChild);
            FifthParent?.AssignChild(FirstChild, SecondChild, ThirdChild, FourthChild, FifthChild);

            FirstChild?.AssignParent(FirstParent, SecondParent, ThirdParent, FourthParent, FifthParent);
            SecondChild?.AssignParent(FirstParent, SecondParent, ThirdParent, FourthParent, FifthParent);
            ThirdChild?.AssignParent(FirstParent, SecondParent, ThirdParent, FourthParent, FifthParent);
            FourthChild?.AssignParent(FirstParent, SecondParent, ThirdParent, FourthParent, FifthParent);
            FifthChild?.AssignParent(FirstParent, SecondParent, ThirdParent, FourthParent, FifthParent);
        }

        public void ReverseChildParent()
        {
            NetNode temp;

            temp = FirstChild; FirstChild = FirstParent; FirstParent = temp;
            temp = SecondChild; SecondChild = SecondParent; SecondParent = temp;
            temp = ThirdChild; ThirdChild = ThirdParent; ThirdParent = temp;
            temp = FourthChild; FourthChild = FourthParent; FourthParent = temp;
            temp = FifthChild; FifthChild = FifthParent; FifthParent = temp;
        }
    }
}
