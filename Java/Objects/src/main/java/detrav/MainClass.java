package detrav;


public class MainClass {
    public static void main(String[] args) {
        ForJIT();
        //JIT
        System.out.println("Start Benchmark");
        ForJIT();
    }

    private static void ForJIT() {
        Watcher watch = new Watcher(new String[]
                {
                        "Count 100000",
                        "Count 100000 repl layer 4",
                        "Count 100000 repl every 3",
                        "Count 100000 repl every 2",
                        "Count 100000 repl 1/3 over 1/4",
                        "Count 100000 remove every 3",
                        "Count 100000 fill to full 1",
                        "Count 100000 remove every 2",
                        "Count 100000 fill to full 2",
                        "Count 100000 reverse",
                        "Count 100000 full init",
                        "Count 100000 full init repl layer 4",
                        "Count 100000 full init repl every 3",
                        "Count 100000 full init repl every 2",
                        "Count 100000 full init repl 1/3 over 1/4",
                        "Count 100000 full init remove every 3",
                        "Count 100000 full init fill to full 1",
                        "Count 100000 full init remove every 2",
                        "Count 100000 full init fill to full 2",
                        "Count 100000 full init reverse",
                }, 100);
        TestObjects(0, false, 100000, watch);
        TestObjects(10, true, 100000, watch);
        watch.Stop();
    }

    private static void TestObjects(int column, Boolean fullInit, int count, Watcher watch) {
        for (int j = 0; j < 100; j++) {
            watch.ReStart();
            //Test1
            NetNode root1, root2, root3, root4, root5;
            NetNode netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5;
            NetNode[] netNodes = InitNewLine(fullInit, null, null, null, null, null);
            netNodeLast1 = root1 = netNodes[0];
            netNodeLast2 = root2 = netNodes[1];
            netNodeLast3 = root3 = netNodes[2];
            netNodeLast4 = root4 = netNodes[3];
            netNodeLast5 = root5 = netNodes[4];
            for (int i = 1; i < count; i++) {
                netNodes = InitNewLine(fullInit, netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5);
                netNodeLast1 = netNodes[0];
                netNodeLast2 = netNodes[1];
                netNodeLast3 = netNodes[2];
                netNodeLast4 = netNodes[3];
                netNodeLast5 = netNodes[4];
            }
            watch.AddAndReset(column, j,root1);
            //test2
            NetNode replace4 = root4;
            while (replace4 != null) {
                NetNode newReplace4 = new NetNode(fullInit);
                newReplace4.Assign(replace4);
                replace4 = newReplace4.getfourthChild();
            }
            watch.AddAndReset(column + 1, j,root1);
            //test3
            netNodeLast1 = root1;
            for (int i = 0; netNodeLast1 != null; i++) {
                if ((i + 1) % 3 == 0) {
                    NetNode newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1);
                    netNodeLast1 = newNode;
                    newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1.getFirstNeighbor());
                    newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1.getSecondNeighbor());
                    newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1.getThirdNeighbor());
                    newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1.getfourthNeighbor());
                }
                netNodeLast1 = netNodeLast1.getFirstChild();
            }
            watch.AddAndReset(column + 2, j,root1);
            //test4
            netNodeLast1 = root1;
            for (int i = 0; netNodeLast1 != null; i++) {
                if ((i + 1) % 2 == 0) {
                    NetNode newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1);
                    netNodeLast1 = newNode;
                    newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1.getFirstNeighbor());
                    newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1.getSecondNeighbor());
                    newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1.getThirdNeighbor());
                    newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1.getfourthNeighbor());
                }
                netNodeLast1 = netNodeLast1.getFirstChild();
            }
            watch.AddAndReset(column + 3, j,root1);
            //test5
            netNodeLast1 = root1;
            for (int i = 0; netNodeLast1 != null; i++) {
                if (i >= count / 4 && i < count / 4 + count / 3) {
                    NetNode newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1);
                    netNodeLast1 = newNode;
                    newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1.getFirstNeighbor());
                    newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1.getSecondNeighbor());
                    newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1.getThirdNeighbor());
                    newNode = new NetNode(fullInit);
                    newNode.Assign(netNodeLast1.getfourthNeighbor());
                }
                netNodeLast1 = netNodeLast1.getFirstChild();
            }
            watch.AddAndReset(column + 4, j,root1);
            //test6
            netNodeLast1 = root1;
            for (int i = 0; netNodeLast1 != null; i++) {
                if ((i + 1) % 3 == 0) {
                    NetNode temp = netNodeLast1;
                    netNodeLast1 = temp.getFirstChild();
                    temp.RemoveRaw();
                } else netNodeLast1 = netNodeLast1.getFirstChild();
            }
            watch.AddAndReset(column + 5, j,root1);
            //test7
            netNodeLast1 = root1;
            netNodeLast2 = root2;
            netNodeLast3 = root3;
            netNodeLast4 = root4;
            netNodeLast5 = root5;
            for (int i = 0; i < count; i++) {
                if (netNodeLast1.getFifthChild() == null) {
                    netNodes = InitNewLine(fullInit, netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5);
                    netNodeLast1 = netNodes[0];
                    netNodeLast2 = netNodes[1];
                    netNodeLast3 = netNodes[2];
                    netNodeLast4 = netNodes[3];
                    netNodeLast5 = netNodes[4];
                } else {
                    netNodeLast1 = netNodeLast1.getFirstChild();
                    netNodeLast2 = netNodeLast2.getSecondChild();
                    netNodeLast3 = netNodeLast3.getThirdChild();
                    netNodeLast4 = netNodeLast4.getfourthChild();
                    netNodeLast5 = netNodeLast5.getFifthChild();
                }
            }
            watch.AddAndReset(column + 6, j,root1);
            //test8
            netNodeLast1 = root1;
            for (int i = 0; netNodeLast1 != null; i++) {
                if ((i + 1) % 2 == 0) {
                    NetNode temp = netNodeLast1;
                    netNodeLast1 = temp.getFirstChild();
                    temp.RemoveRaw();
                } else netNodeLast1 = netNodeLast1.getFirstChild();
            }
            watch.AddAndReset(column + 7, j,root1);
            //test9
            netNodeLast1 = root1;
            netNodeLast2 = root2;
            netNodeLast3 = root3;
            netNodeLast4 = root4;
            netNodeLast5 = root5;
            for (int i = 0; i < count; i++) {
                if (netNodeLast1.getFirstChild() == null) {
                    netNodes = InitNewLine(fullInit, netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5);
                    netNodeLast1 = netNodes[0];
                    netNodeLast2 = netNodes[1];
                    netNodeLast3 = netNodes[2];
                    netNodeLast4 = netNodes[3];
                    netNodeLast5 = netNodes[4];
                } else {
                    netNodeLast1 = netNodeLast1.getFirstChild();
                    netNodeLast2 = netNodeLast2.getSecondChild();
                    netNodeLast3 = netNodeLast3.getThirdChild();
                    netNodeLast4 = netNodeLast4.getfourthChild();
                    netNodeLast5 = netNodeLast5.getFifthChild();
                }
            }
            watch.AddAndReset(column + 8, j,root1);
            //test10
            netNodeLast1 = root1;
            netNodeLast2 = root2;
            netNodeLast3 = root3;
            netNodeLast4 = root4;
            netNodeLast5 = root5;
            int k = 0;
            while (netNodeLast1 != null) {
                k++;
                netNodeLast1.ReverseChildParent();
                netNodeLast1 = netNodeLast1.getFirstParent();
                netNodeLast2.ReverseChildParent();
                netNodeLast2 = netNodeLast2.getSecondParent();
                netNodeLast3.ReverseChildParent();
                netNodeLast3 = netNodeLast3.getThirdParent();
                netNodeLast4.ReverseChildParent();
                netNodeLast4 = netNodeLast4.getfourthParent();
                netNodeLast5.ReverseChildParent();
                netNodeLast5 = netNodeLast5.getFifthParent();
            }
            watch.AddAndReset(column + 9, j,root1);
        }
    }

    private static NetNode[] InitNewLine(boolean fullInit, NetNode netNodeLast1, NetNode netNodeLast2, NetNode netNodeLast3, NetNode netNodeLast4, NetNode netNodeLast5) {
        NetNode netNode1 = new NetNode(fullInit);
        NetNode netNode2 = new NetNode(fullInit);
        NetNode netNode3 = new NetNode(fullInit);
        NetNode netNode4 = new NetNode(fullInit);
        NetNode netNode5 = new NetNode(fullInit);

        if (netNodeLast1 != null)
            netNodeLast1.AssignChild(netNode1, netNode2, netNode3, netNode4, netNode5);
        if (netNodeLast2 != null)
            netNodeLast2.AssignChild(netNode1, netNode2, netNode3, netNode4, netNode5);
        if (netNodeLast3 != null)
            netNodeLast3.AssignChild(netNode1, netNode2, netNode3, netNode4, netNode5);
        if (netNodeLast4 != null)
            netNodeLast4.AssignChild(netNode1, netNode2, netNode3, netNode4, netNode5);
        if (netNodeLast5 != null)
            netNodeLast5.AssignChild(netNode1, netNode2, netNode3, netNode4, netNode5);

        netNode1.AssignParent(netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5);
        netNode2.AssignParent(netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5);
        netNode3.AssignParent(netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5);
        netNode4.AssignParent(netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5);
        netNode5.AssignParent(netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5);

        netNode1.AssignNeighbor(netNode2, netNode3, netNode4, netNode5);
        netNode2.AssignNeighbor(netNode1, netNode3, netNode4, netNode5);
        netNode3.AssignNeighbor(netNode1, netNode2, netNode4, netNode5);
        netNode4.AssignNeighbor(netNode1, netNode2, netNode3, netNode5);
        netNode5.AssignNeighbor(netNode1, netNode2, netNode3, netNode4);

        return new NetNode[]{netNode1, netNode2, netNode3, netNode4, netNode5};
    }
}

