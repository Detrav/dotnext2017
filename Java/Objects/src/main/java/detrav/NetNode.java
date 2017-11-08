package detrav;

import java.util.ArrayList;
import java.util.HashMap;

class NetNode
    {
        private NetNode firstChild;
        private NetNode secondChild;
        private NetNode thirdChild;
        private NetNode fourthChild;
        private NetNode fifthChild;
        private NetNode firstParent;
        private NetNode secondParent;
        private NetNode thirdParent;
        private NetNode fourthParent;
        private NetNode fifthParent;
        private NetNode firstNeighbor;
        private NetNode secondNeighbor;
        private NetNode thirdNeighbor;
        private NetNode fourthNeighbor;

        private Object[] array;
        private ArrayList<Object> list;
        private HashMap<String, Object> dictionary;
        private StringBuilder builder;
        private String string;

        public NetNode(boolean fullinit)
        {
            if(fullinit) {
                array = new Object[0];
                list = new ArrayList<Object>();
                dictionary = new HashMap<String, Object>();
                builder = new StringBuilder();
                string = "b165579034d5538683ed211ede1e9d64f68143eb59258763ff11d7a908775a76e86eb7155379fb568efab478ce0cb65a2c7f1787dbcaf1a421f7bca91d2c75aedd72455067c08d9b4b997de52de34b071dc1a70cce130914378bc070f10671ba74fa080cbdd8ca48ca0d34b06b18794a37cba63a6b0e3e44c7ad0e628f4468528255e8e565684dc77dcfe794093b2e4e6d608f63956a7eeb2cd9b5308145532718267cf878680ec2b8851a20a577a1e62f9fc746d423fa0942adad5651c247d6a0615d63328d8f1f4e3860c592aab2257c9f4e46c69758e60cd545f930833b81025c61a6acc074f0349e4c02cfb46030d762310e16b88e2f08fe29c413f66609";
            }
        }

        public NetNode()
        {
            array = new Object[0];
            list = new ArrayList<Object>();
            dictionary = new HashMap<String, Object>();
            builder = new StringBuilder();
            string = "b165579034d5538683ed211ede1e9d64f68143eb59258763ff11d7a908775a76e86eb7155379fb568efab478ce0cb65a2c7f1787dbcaf1a421f7bca91d2c75aedd72455067c08d9b4b997de52de34b071dc1a70cce130914378bc070f10671ba74fa080cbdd8ca48ca0d34b06b18794a37cba63a6b0e3e44c7ad0e628f4468528255e8e565684dc77dcfe794093b2e4e6d608f63956a7eeb2cd9b5308145532718267cf878680ec2b8851a20a577a1e62f9fc746d423fa0942adad5651c247d6a0615d63328d8f1f4e3860c592aab2257c9f4e46c69758e60cd545f930833b81025c61a6acc074f0349e4c02cfb46030d762310e16b88e2f08fe29c413f66609";
        }

        public void AssignChild(NetNode fisrtChild,NetNode secondChild, NetNode thirdChild, NetNode fourthChild, NetNode fifthChild)
        {
            this.firstChild = fisrtChild;
            this.secondChild = secondChild;
            this.thirdChild = thirdChild;
            this.fourthChild = fourthChild;
            this.fifthChild = fifthChild;
        }

        public void AssignParent(NetNode fisrtParent, NetNode secondParent, NetNode thirdParent, NetNode fourthParent, NetNode fifthParent)
        {
            this.firstParent = fisrtParent;
            this.secondParent = secondParent;
            this.thirdParent = thirdParent;
            this.fourthParent = fourthParent;
            this.fifthParent = fifthParent;
        }

        public void AssignNeighbor(NetNode fisrtNeighbor, NetNode secondNeighbor, NetNode thirdNeighbor, NetNode fourthNeighbor)
        {
            this.firstNeighbor = fisrtNeighbor;
            this.secondNeighbor = secondNeighbor;
            this.thirdNeighbor = thirdNeighbor;
            this.fourthNeighbor = fourthNeighbor;
        }

        public void Assign(NetNode reference) {
            firstParent = reference.firstParent;
            secondParent = reference.secondParent;
            thirdParent = reference.thirdParent;
            fourthParent = reference.fourthParent;
            fifthParent = reference.fifthParent;

            firstChild = reference.firstChild;
            secondChild = reference.secondChild;
            thirdChild = reference.thirdChild;
            fourthChild = reference.fourthChild;
            fifthChild = reference.fifthChild;

            firstNeighbor = reference.firstNeighbor;
            secondNeighbor = reference.secondNeighbor;
            thirdNeighbor = reference.thirdNeighbor;
            fourthNeighbor = reference.fourthNeighbor;

            if (firstParent != null)
                firstParent.ReplaceChild(reference, this);
            if (secondParent != null)
                secondParent.ReplaceChild(reference, this);
            if (thirdParent != null)
                thirdParent.ReplaceChild(reference, this);
            if (fourthParent != null)
                fourthParent.ReplaceChild(reference, this);
            if (fifthParent != null)
                fifthParent.ReplaceChild(reference, this);

            if (firstChild != null)
                firstChild.ReplaceParent(reference, this);
            if (secondChild != null)
                secondChild.ReplaceParent(reference, this);
            if (thirdChild != null)
                thirdChild.ReplaceParent(reference, this);
            if (fourthChild != null)
                fourthChild.ReplaceParent(reference, this);
            if (fifthChild != null)
                fifthChild.ReplaceParent(reference, this);

            firstNeighbor.ReplaceNeighbor(reference, this);
            secondNeighbor.ReplaceNeighbor(reference, this);
            thirdNeighbor.ReplaceNeighbor(reference, this);
            fourthNeighbor.ReplaceNeighbor(reference, this);
        }

        public void ReplaceChild(NetNode from, NetNode to)
        {
            if (firstChild == from)
                firstChild = to;
            else if (secondChild == from)
                secondChild = to;
            else if (thirdChild == from)
                thirdChild = to;
            else if (fourthChild == from)
                fourthChild = to;
            else if (fifthChild == from)
                fifthChild = to;
        }


        public void ReplaceNeighbor(NetNode from, NetNode to)
        {
            if (firstNeighbor == from)
                firstNeighbor = to;
            else if (secondNeighbor == from)
                secondNeighbor = to;
            else if (thirdNeighbor == from)
                thirdNeighbor = to;
            else if (fourthNeighbor == from)
                fourthNeighbor = to;
        }


        public void ReplaceParent(NetNode from, NetNode to)
        {
            if (firstParent == from)
                firstParent = to;
            else if (secondParent == from)
                secondParent = to;
            else if (thirdParent == from)
                thirdParent = to;
            else if (fourthParent == from)
                fourthParent = to;
            else if (fifthParent == from)
                fifthParent = to;
        }

        public void RemoveRaw()
        {
            if(firstParent!=null)
            firstParent.AssignChild(firstChild, secondChild, thirdChild, fourthChild, fifthChild);
            if(secondParent!=null)
            secondParent.AssignChild(firstChild, secondChild, thirdChild, fourthChild, fifthChild);
            if(thirdParent!=null)
            thirdParent.AssignChild(firstChild, secondChild, thirdChild, fourthChild, fifthChild);
            if(fourthParent!=null)
            fourthParent.AssignChild(firstChild, secondChild, thirdChild, fourthChild, fifthChild);
            if(fifthParent!=null)
            fifthParent.AssignChild(firstChild, secondChild, thirdChild, fourthChild, fifthChild);

            if(firstChild!=null)
            firstChild.AssignParent(firstParent, secondParent, thirdParent, fourthParent, fifthParent);
            if(secondChild!=null)
            secondChild.AssignParent(firstParent, secondParent, thirdParent, fourthParent, fifthParent);
            if(thirdChild!=null)
            thirdChild.AssignParent(firstParent, secondParent, thirdParent, fourthParent, fifthParent);
            if(fourthChild!=null)
            fourthChild.AssignParent(firstParent, secondParent, thirdParent, fourthParent, fifthParent);
            if(fifthChild!=null)
            fifthChild.AssignParent(firstParent, secondParent, thirdParent, fourthParent, fifthParent);
        }

        public void ReverseChildParent()
        {
            NetNode temp;

            temp = firstChild; firstChild = firstParent; firstParent = temp;
            temp = secondChild; secondChild = secondParent; secondParent = temp;
            temp = thirdChild; thirdChild = thirdParent; thirdParent = temp;
            temp = fourthChild; fourthChild = fourthParent; fourthParent = temp;
            temp = fifthChild; fifthChild = fifthParent; fifthParent = temp;
        }

        public NetNode getFirstChild() {
            return firstChild;
        }

        public void setFirstChild(NetNode firstChild) {
            this.firstChild = firstChild;
        }

        public NetNode getSecondChild() {
            return secondChild;
        }

        public void setSecondChild(NetNode secondChild) {
            this.secondChild = secondChild;
        }

        public NetNode getThirdChild() {
            return thirdChild;
        }

        public void setThirdChild(NetNode thirdChild) {
            this.thirdChild = thirdChild;
        }

        public NetNode getfourthChild() {
            return fourthChild;
        }

        public void setfourthChild(NetNode fourthChild) {
            this.fourthChild = fourthChild;
        }

        public NetNode getFifthChild() {
            return fifthChild;
        }

        public void setFifthChild(NetNode fifthChild) {
            this.fifthChild = fifthChild;
        }

        public NetNode getFirstParent() {
            return firstParent;
        }

        public void setFirstParent(NetNode firstParent) {
            this.firstParent = firstParent;
        }

        public NetNode getSecondParent() {
            return secondParent;
        }

        public void setSecondParent(NetNode secondParent) {
            this.secondParent = secondParent;
        }

        public NetNode getThirdParent() {
            return thirdParent;
        }

        public void setThirdParent(NetNode thirdParent) {
            this.thirdParent = thirdParent;
        }

        public NetNode getfourthParent() {
            return fourthParent;
        }

        public void setfourthParent(NetNode fourthParent) {
            this.fourthParent = fourthParent;
        }

        public NetNode getFifthParent() {
            return fifthParent;
        }

        public void setFifthParent(NetNode fifthParent) {
            this.fifthParent = fifthParent;
        }

        public NetNode getFirstNeighbor() {
            return firstNeighbor;
        }

        public void setFirstNeighbor(NetNode firstNeighbor) {
            this.firstNeighbor = firstNeighbor;
        }

        public NetNode getSecondNeighbor() {
            return secondNeighbor;
        }

        public void setSecondNeighbor(NetNode secondNeighbor) {
            this.secondNeighbor = secondNeighbor;
        }

        public NetNode getThirdNeighbor() {
            return thirdNeighbor;
        }

        public void setThirdNeighbor(NetNode thirdNeighbor) {
            this.thirdNeighbor = thirdNeighbor;
        }

        public NetNode getfourthNeighbor() {
            return fourthNeighbor;
        }

        public void setfourthNeighbor(NetNode fourthNeighbor) {
            this.fourthNeighbor = fourthNeighbor;
        }

        public Object[] getArray() {
            return array;
        }

        public void setArray(Object[] array) {
            this.array = array;
        }

        public ArrayList<Object> getList() {
            return list;
        }

        public void setList(ArrayList<Object> list) {
            this.list = list;
        }

        public HashMap<String, Object> getDictionary() {
            return dictionary;
        }

        public void setDictionary(HashMap<String, Object> dictionary) {
            this.dictionary = dictionary;
        }

        public StringBuilder getBuilder() {
            return builder;
        }

        public void setBuilder(StringBuilder builder) {
            this.builder = builder;
        }

        public String getString() {
            return string;
        }

        public void setString(String string) {
            this.string = string;
        }

        
    }
