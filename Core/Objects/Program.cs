using System;

namespace Objects
{
    class Program
    {
        static void Main(string[] args)
        {
            Watcher watch = new Watcher(new string[]
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

        private static void TestObjects(int column, bool fullInit, int count, Watcher watch)
        {
            for (int j = 0; j < 100; j++)
            {
                //Start
                watch.ReStart();
                //Test1 Создание сети объектов
                NetNode root1, root2, root3, root4, root5;
                NetNode netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5;
                InitNewLine(fullInit, null, null, null, null, null, out root1, out root2, out root3, out root4, out root5);
                netNodeLast1 = root1;
                netNodeLast2 = root2;
                netNodeLast3 = root3;
                netNodeLast4 = root4;
                netNodeLast5 = root5;
                for (int i = 1; i < count; i++)
                {
                    InitNewLine(fullInit, netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5,
                        out netNodeLast1, out netNodeLast2, out netNodeLast3, out netNodeLast4, out netNodeLast5);
                }
                watch.AddAndReset(column, j, root1);
                //test2 Замена среза по четвертому объекту вдоль сети на новые.
                NetNode replace4 = root4;
                while (replace4 != null)
                {
                    NetNode newReplace4 = new NetNode(fullInit);
                    newReplace4.Assign(replace4);
                    replace4 = newReplace4.FourthChild;
                }
                watch.AddAndReset(column + 1, j, root1);
                //test3 Замена каждого третьего перпендикулярного среза.
                netNodeLast1 = root1;
                for (int i = 0; netNodeLast1 != null; i++)
                {
                    if ((i + 1) % 3 == 0)
                    {
                        NetNode newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1);
                        netNodeLast1 = newNode;
                        newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1.FirstNeighbor);
                        newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1.SecondNeighbor);
                        newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1.ThirdNeighbor);
                        newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1.FourthNeighbor);
                    }
                    netNodeLast1 = netNodeLast1.FirstChild;
                }
                watch.AddAndReset(column + 2, j, root1);
                //test4 Замена каждого второго перпендикулярного среза.
                netNodeLast1 = root1;
                for (int i = 0; netNodeLast1 != null; i++)
                {
                    if ((i + 1) % 2 == 0)
                    {
                        NetNode newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1);
                        netNodeLast1 = newNode;
                        newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1.FirstNeighbor);
                        newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1.SecondNeighbor);
                        newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1.ThirdNeighbor);
                        newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1.FourthNeighbor);
                    }
                    netNodeLast1 = netNodeLast1.FirstChild;
                }
                watch.AddAndReset(column + 3, j, root1);
                //test5 Замена третьи перпендикулярных срезов после четверти объектов.
                netNodeLast1 = root1;
                for (int i = 0; netNodeLast1 != null; i++)
                {
                    if (i >= count / 4 && i < count / 4 + count / 3)
                    {
                        NetNode newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1);
                        netNodeLast1 = newNode;
                        newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1.FirstNeighbor);
                        newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1.SecondNeighbor);
                        newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1.ThirdNeighbor);
                        newNode = new NetNode(fullInit);
                        newNode.Assign(netNodeLast1.FourthNeighbor);
                    }
                    netNodeLast1 = netNodeLast1.FirstChild;
                }
                watch.AddAndReset(column + 4, j, root1);
                //test6 Удаление каждого третьего среза.
                netNodeLast1 = root1;
                for (int i = 0; netNodeLast1 != null; i++)
                {
                    if ((i + 1) % 3 == 0)
                    {
                        NetNode temp = netNodeLast1;
                        netNodeLast1 = temp.FirstChild;
                        temp.RemoveRaw();
                    }
                    else netNodeLast1 = netNodeLast1.FirstChild;
                }
                watch.AddAndReset(column + 5, j, root1);
                //test7 Дополнение сети до полной.
                netNodeLast1 = root1;
                netNodeLast2 = root2;
                netNodeLast3 = root3;
                netNodeLast4 = root4;
                netNodeLast5 = root5;
                for (int i = 0; i < count; i++)
                {
                    if (netNodeLast1.FirstChild == null)
                    {
                        InitNewLine(fullInit, netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5,
                        out netNodeLast1, out netNodeLast2, out netNodeLast3, out netNodeLast4, out netNodeLast5);
                    }
                    else
                    {
                        netNodeLast1 = netNodeLast1.FirstChild;
                        netNodeLast2 = netNodeLast2.SecondChild;
                        netNodeLast3 = netNodeLast3.ThirdChild;
                        netNodeLast4 = netNodeLast4.FourthChild;
                        netNodeLast5 = netNodeLast5.FifthChild;
                    }
                }
                watch.AddAndReset(column + 6, j, root1);
                //test8 Удаление каждого второго среза.
                netNodeLast1 = root1;
                for (int i = 0; netNodeLast1 != null; i++)
                {
                    if ((i + 1) % 2 == 0)
                    {
                        NetNode temp = netNodeLast1;
                        netNodeLast1 = temp.FirstChild;
                        temp.RemoveRaw();
                    }
                    else netNodeLast1 = netNodeLast1.FirstChild;
                }
                watch.AddAndReset(column + 7, j, root1);
                //test9 Дополнение сети до полной.
                netNodeLast1 = root1;
                netNodeLast2 = root2;
                netNodeLast3 = root3;
                netNodeLast4 = root4;
                netNodeLast5 = root5;
                for (int i = 0; i < count; i++)
                {
                    if (netNodeLast1.FirstChild == null)
                    {
                        InitNewLine(fullInit, netNodeLast1, netNodeLast2, netNodeLast3, netNodeLast4, netNodeLast5,
                        out netNodeLast1, out netNodeLast2, out netNodeLast3, out netNodeLast4, out netNodeLast5);
                    }
                    else
                    {
                        netNodeLast1 = netNodeLast1.FirstChild;
                        netNodeLast2 = netNodeLast2.SecondChild;
                        netNodeLast3 = netNodeLast3.ThirdChild;
                        netNodeLast4 = netNodeLast4.FourthChild;
                        netNodeLast5 = netNodeLast5.FifthChild;
                    }
                }
                watch.AddAndReset(column + 8, j, root1);
                //test10  реверс.
                netNodeLast1 = root1;
                netNodeLast2 = root2;
                netNodeLast3 = root3;
                netNodeLast4 = root4;
                netNodeLast5 = root5;
                int k = 0;
                while (netNodeLast1 != null)
                {
                    k++;
                    netNodeLast1.ReverseChildParent();
                    netNodeLast1 = netNodeLast1.FirstParent;
                    netNodeLast2.ReverseChildParent();
                    netNodeLast2 = netNodeLast2.SecondParent;
                    netNodeLast3.ReverseChildParent();
                    netNodeLast3 = netNodeLast3.ThirdParent;
                    netNodeLast4.ReverseChildParent();
                    netNodeLast4 = netNodeLast4.FourthParent;
                    netNodeLast5.ReverseChildParent();
                    netNodeLast5 = netNodeLast5.FifthParent;
                }
                watch.AddAndReset(column + 9, j, root1);
            }
        }

        private static void InitNewLine(bool fullInit, NetNode netNodeLast1, NetNode netNodeLast2, NetNode netNodeLast3, NetNode netNodeLast4, NetNode netNodeLast5,
            out NetNode netNode1, out NetNode netNode2, out NetNode netNode3, out NetNode netNode4, out NetNode netNode5)
        {
            netNode1 = new NetNode(fullInit);
            netNode2 = new NetNode(fullInit);
            netNode3 = new NetNode(fullInit);
            netNode4 = new NetNode(fullInit);
            netNode5 = new NetNode(fullInit);

            netNodeLast1?.AssignChild(netNode1, netNode2, netNode3, netNode4, netNode5);
            netNodeLast2?.AssignChild(netNode1, netNode2, netNode3, netNode4, netNode5);
            netNodeLast3?.AssignChild(netNode1, netNode2, netNode3, netNode4, netNode5);
            netNodeLast4?.AssignChild(netNode1, netNode2, netNode3, netNode4, netNode5);
            netNodeLast5?.AssignChild(netNode1, netNode2, netNode3, netNode4, netNode5);

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
        }
    }
}