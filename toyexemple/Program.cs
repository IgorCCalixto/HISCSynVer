
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UltraDES;

namespace toyexemple
{
    internal class Program
    {

        private static void toyexemple(out List<DeterministicFiniteAutomaton> plants, out List<DeterministicFiniteAutomaton> specs, out List<DeterministicFiniteAutomaton> interfaces)
        {

            //cria duas variaveis para conter uma lista de plantas e especificações respectivamente
            plants = new List<DeterministicFiniteAutomaton>();
            specs = new List<DeterministicFiniteAutomaton>();
            interfaces = new List<DeterministicFiniteAutomaton>();

            //cria estados setando a quantidade a partir da função range e selecionando o que quer
            var s = Enumerable.Range(0, 51).Select(
                    k => new State(k.ToString(),
                        k == 0
                            ? Marking.Marked
                            : Marking.Unmarked))
                    .ToArray();
            s[49] = new State("49", Marking.Marked);
            s[50] = new State("50", Marking.Marked);
            //cria eventos 

            //GERENTE/ROBO
            Event robop1p2 = new Event("robop1p2", Controllability.Controllable);
            Event robod1d2 = new Event("robod1d2", Controllability.Uncontrollable);
            Event robop2p3 = new Event("robop2p3", Controllability.Controllable);
            Event robod2d3 = new Event("robod2d3", Controllability.Uncontrollable);
            Event robop2a = new Event("robop2a", Controllability.Controllable);
            Event robod2a = new Event("robod2a", Controllability.Uncontrollable);
            Event robop3a = new Event("robop3a", Controllability.Controllable);
            Event robod3a = new Event("robod3a", Controllability.Uncontrollable);

            //Impressão
            Event IDEFVAR = new Event("IDEFVAR", Controllability.Controllable);
            Event FDEFVAR = new Event("FDEFVAR", Controllability.Uncontrollable);
            Event IFATOBJ = new Event("IFATOBJ", Controllability.Controllable);
            Event FFATOBJ = new Event("FFATOBJ", Controllability.Uncontrollable);
            Event IIMP = new Event("IIMP", Controllability.Controllable);
            Event FIMP = new Event("FIMP", Controllability.Uncontrollable);
            Event IPIMP = new Event("IPIMP", Controllability.Controllable);
            Event FPIMP = new Event("FPIMP", Controllability.Controllable);

            //CORTE
            Event ISCANOBJ = new Event("ISCANOBJ", Controllability.Controllable);
            Event FSCANOBJ = new Event("FSCANOBJ", Controllability.Uncontrollable);
            Event ITESTOBJ = new Event("ITESTOBJ", Controllability.Controllable);
            Event FTESTOBJ = new Event("FTESTOBJ", Controllability.Uncontrollable);
            Event ERROCORTE = new Event("ERROCORTE", Controllability.Uncontrollable);
            Event ICORTE = new Event("ICORTE", Controllability.Controllable);
            Event FCORTE = new Event("FCORTE", Controllability.Uncontrollable);
            Event IPCORTE = new Event("IPCORTE", Controllability.Controllable);
            Event FPCORTE = new Event("FPCORTE", Controllability.Controllable);

            //PINTURA
            Event ICORRIOBJ = new Event("ICORRIOBJ", Controllability.Controllable);
            Event FCORRIOBJ = new Event("FCORRIOBJ", Controllability.Uncontrollable);
            Event IPIN = new Event("IPIN", Controllability.Controllable);
            Event FPIN = new Event("FPIN", Controllability.Uncontrollable);
            Event IPPIN = new Event("IPPIN", Controllability.Controllable);
            Event FPPIN = new Event("FPPIN", Controllability.Controllable);


            /////////////////////////////////Plantas
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("Plants:");

            //////HIGHLEVEL

            ////H11

            //GERENTEROBO plants[0]
            var GERENTEROBO = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], robop1p2, s[1]),
                                    new Transition(s[1], robod1d2, s[0]),
                                    new Transition(s[0], robop2p3, s[2]),
                                    new Transition(s[2], robod2d3, s[0]),
                                    new Transition(s[0], robop2a, s[3]),
                                    new Transition(s[3], robod2a, s[0]),
                                    new Transition(s[0], robop3a, s[4]),
                                    new Transition(s[4], robod3a, s[0]),
                      }, s[0], "GERENTEROBO");

            plants.Add(GERENTEROBO);

            Console.WriteLine("\tAutomaton: {0}", GERENTEROBO.ToString());
            Console.WriteLine("\tStates: {0}", GERENTEROBO.Size);
            Console.WriteLine("\tTransitions: {0}", GERENTEROBO.Transitions.Count());
            //PLANTPOSITION.drawSVGFigure("GERENTEROBO", true);      

            //////LOWLEVEL

            //////H21

            //IMP1 plants[1]
            var IMP1 = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], IDEFVAR, s[1]),
                                    new Transition(s[1], IDEFVAR, s[1]),
                                    new Transition(s[1], FDEFVAR, s[2]),
                                    new Transition(s[2], IFATOBJ, s[3]),
                                    new Transition(s[3], IDEFVAR, s[1]),
                                    new Transition(s[3], FFATOBJ, s[0])
                      }, s[0], "IMP1");

            plants.Add(IMP1);

            Console.WriteLine("\tAutomaton: {0}", IMP1.ToString());
            Console.WriteLine("\tStates: {0}", IMP1.Size);
            Console.WriteLine("\tTransitions: {0}", IMP1.Transitions.Count());
            //PLANTPOSITION.drawSVGFigure("IMP1", true);   

            //IMP2 plants[2]
            var IMP2 = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], IIMP, s[1]),
                                    new Transition(s[1], IIMP, s[1]),                                    
                                    new Transition(s[1], FIMP, s[0])
                      }, s[0], "IMP2");

            plants.Add(IMP2);

            Console.WriteLine("\tAutomaton: {0}", IMP2.ToString());
            Console.WriteLine("\tStates: {0}", IMP2.Size);
            Console.WriteLine("\tTransitions: {0}", IMP2.Transitions.Count());
            //PLANTPOSITION.drawSVGFigure("IMP2", true);      

            //IMP3 plants[3]
            var IMP3 = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], IPIMP, s[0]),
                                    new Transition(s[0], FPIMP, s[0])
                      }, s[0], "IMP3");

            plants.Add(IMP3);

            Console.WriteLine("\tAutomaton: {0}", IMP3.ToString());
            Console.WriteLine("\tStates: {0}", IMP3.Size);
            Console.WriteLine("\tTransitions: {0}", IMP3.Transitions.Count());
            //PLANTPOSITION.drawSVGFigure("IMP3", true);  


            //////H22

            //CORTE1 plants[4]
            var CORTE1 = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], ISCANOBJ, s[1]),
                                    new Transition(s[1], ISCANOBJ, s[1]),
                                    new Transition(s[1], FSCANOBJ, s[0]),
                                    new Transition(s[0], ITESTOBJ, s[2]),
                                    new Transition(s[2], FTESTOBJ, s[0]),
                                    new Transition(s[2], ERROCORTE, s[0])
                      }, s[0], "CORTE1");

            plants.Add(CORTE1);

            Console.WriteLine("\tAutomaton: {0}", CORTE1.ToString());
            Console.WriteLine("\tStates: {0}", CORTE1.Size);
            Console.WriteLine("\tTransitions: {0}", CORTE1.Transitions.Count());
            //PLANTPOSITION.drawSVGFigure("CORTE1", true);    

            //CORTE2 plants[5]
            var CORTE2 = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], ICORTE, s[1]),
                                    new Transition(s[1], ICORTE, s[1]),
                                    new Transition(s[1], FCORTE, s[0])
                      }, s[0], "CORTE2");

            plants.Add(CORTE2);

            Console.WriteLine("\tAutomaton: {0}", CORTE2.ToString());
            Console.WriteLine("\tStates: {0}", CORTE2.Size);
            Console.WriteLine("\tTransitions: {0}", CORTE2.Transitions.Count());
            //PLANTPOSITION.drawSVGFigure("CORTE2", true);   

            //CORTE3 plants[6]
            var CORTE3 = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], IPCORTE, s[0]),
                                    new Transition(s[0], FPCORTE, s[0])
                      }, s[0], "CORTE3");

            plants.Add(CORTE3);

            Console.WriteLine("\tAutomaton: {0}", CORTE3.ToString());
            Console.WriteLine("\tStates: {0}", CORTE3.Size);
            Console.WriteLine("\tTransitions: {0}", CORTE3.Transitions.Count());
            //PLANTPOSITION.drawSVGFigure("CORTE3", true);   


            ////H23

            //PIN1 plants[7]
            var PIN1 = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], ICORRIOBJ, s[1]),
                                    new Transition(s[1], ICORRIOBJ, s[1]),
                                    new Transition(s[1], FCORRIOBJ, s[0])
                      }, s[0], "PIN1");

            plants.Add(PIN1);

            Console.WriteLine("\tAutomaton: {0}", PIN1.ToString());
            Console.WriteLine("\tStates: {0}", PIN1.Size);
            Console.WriteLine("\tTransitions: {0}", PIN1.Transitions.Count());
            //PLANTPOSITION.drawSVGFigure("PIN1", true);   

            //PIN2 plants[8]
            var PIN2 = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], IPIN, s[1]),
                                    new Transition(s[1], IPIN, s[1]),
                                    new Transition(s[1], FPIN, s[0])
                      }, s[0], "PIN2");

            plants.Add(PIN2);

            Console.WriteLine("\tAutomaton: {0}", PIN2.ToString());
            Console.WriteLine("\tStates: {0}", PIN2.Size);
            Console.WriteLine("\tTransitions: {0}", PIN2.Transitions.Count());
            //PLANTPOSITION.drawSVGFigure("PIN2", true);   

            //PIN3 plants[9]
            var PIN3 = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], IPPIN, s[0]),
                                    new Transition(s[0], FPPIN, s[0])
                      }, s[0], "PIN3");

            plants.Add(PIN3);

            Console.WriteLine("\tAutomaton: {0}", PIN3.ToString());
            Console.WriteLine("\tStates: {0}", PIN3.Size);
            Console.WriteLine("\tTransitions: {0}", PIN3.Transitions.Count());
            //PLANTPOSITION.drawSVGFigure("PIN3", true);   


            ////////////////////////////especificações (supervisores)
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("Specifications:");

            //HIGHLEVEL

            //H11
            
            //SPECGEN specs[0]
            var SPECGEN = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], IPIMP, s[1]),
                            new Transition(s[1], FPIMP, s[2]),
                            new Transition(s[2], robop1p2, s[3]),
                            new Transition(s[3], robod1d2, s[4]),
                            new Transition(s[4], IPIMP, s[5]),
                            new Transition(s[5], robop1p2, s[6]),
                            new Transition(s[6], FPIMP, s[7]),
                            new Transition(s[7], robod1d2, s[8]),
                            new Transition(s[4], IPCORTE, s[5]),
                            new Transition(s[5], FPCORTE, s[6]),
                            new Transition(s[6], robop2p3, s[7]),
                            new Transition(s[7], robod2d3, s[8]),
                            new Transition(s[8], IPPIN, s[9]),
                            new Transition(s[9], FPPIN, s[10]),
                            new Transition(s[10], robop3a, s[11]),
                            new Transition(s[11], robod3a, s[0]),
                            new Transition(s[5], ERROCORTE, s[12]),
                            new Transition(s[12], robop2a, s[13]),
                            new Transition(s[13], robod2a, s[0])
                        }, s[0], "SPECGEN");

            specs.Add(SPECGEN);

            Console.WriteLine("\tAutomaton: {0}", SPECGEN.ToString());
            Console.WriteLine("\tStates: {0}", SPECGEN.Size);
            Console.WriteLine("\tTransitions: {0}", SPECGEN.Transitions.Count());

            //LOWLEVEL

            //////H21

            //SPECIMP specs[1]
            var SPECIMP = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], IPIMP, s[1]),
                            new Transition(s[1], IDEFVAR, s[2]),
                            new Transition(s[2], FDEFVAR, s[3]),
                            new Transition(s[3], IFATOBJ, s[4]),
                            new Transition(s[4], FFATOBJ, s[5]),
                            new Transition(s[5], IIMP, s[6]),
                            new Transition(s[6], FIMP, s[7]),
                            new Transition(s[7], FPIMP, s[0])
                        }, s[0], "SPECIMP");

            specs.Add(SPECIMP);

            Console.WriteLine("\tAutomaton: {0}", SPECIMP.ToString());
            Console.WriteLine("\tStates: {0}", SPECIMP.Size);
            Console.WriteLine("\tTransitions: {0}", SPECIMP.Transitions.Count());
            

            ////H22

            //SPECCORTE specs[2]
            var SPECCORTE = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], IPCORTE, s[1]),
                            new Transition(s[1], ISCANOBJ, s[2]),
                            new Transition(s[2], FSCANOBJ, s[3]),

                            //new Transition(s[3], IPCORTE, s[10]),
                            //new Transition(s[10], ERROCORTE, s[11]),
                            //new Transition(s[11], IPCORTE, s[12]),
                            //new Transition(s[12], FPCORTE, s[49]),

                            new Transition(s[3], ICORTE, s[4]),
                            new Transition(s[4], FCORTE, s[5]),
                            new Transition(s[5], ISCANOBJ, s[6]),
                            new Transition(s[6], FSCANOBJ, s[7]),
                            new Transition(s[7], ITESTOBJ, s[8]),
                            new Transition(s[8], FTESTOBJ, s[9]),

                            //new Transition(s[9], IPCORTE, s[13]),
                            //new Transition(s[13], FPCORTE, s[50]),

                            new Transition(s[9], FPCORTE, s[0]),
                            new Transition(s[8], ERROCORTE, s[0])
                        }, s[0], "SPECCORTE");

            specs.Add(SPECCORTE);

            Console.WriteLine("\tAutomaton: {0}", SPECCORTE.ToString());
            Console.WriteLine("\tStates: {0}", SPECCORTE.Size);
            Console.WriteLine("\tTransitions: {0}", SPECCORTE.Transitions.Count());

            
            ////H23

            //SPECPIN specs[3]
            var SPECPIN = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], IPPIN, s[1]),
                            new Transition(s[1], ICORRIOBJ, s[2]),
                            new Transition(s[2], FCORRIOBJ, s[3]),
                            new Transition(s[3], IPIN, s[4]),
                            new Transition(s[4], FPIN, s[5]),
                            new Transition(s[5], FPPIN, s[0])
                        }, s[0], "SPECPIN");

            specs.Add(SPECPIN);

            Console.WriteLine("\tAutomaton: {0}", SPECPIN.ToString());
            Console.WriteLine("\tStates: {0}", SPECPIN.Size);
            Console.WriteLine("\tTransitions: {0}", SPECPIN.Transitions.Count());

           

            ////////////////////////////Interfaces

            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("Interfaces Parallel Case:");

           
            //Interface I21 
            var SI21 = new DeterministicFiniteAutomaton(new[]
           {
                            new Transition(s[0], IPIMP, s[1]),
                            new Transition(s[1], FPIMP, s[0])
                        }, s[0], "SI21");//nome do automato

            interfaces.Add(SI21);

            Console.WriteLine("\tAutomaton: {0}", SI21.ToString());

            //Interface I22
            var SI22 = new DeterministicFiniteAutomaton(new[]
           {
                             new Transition(s[0], IPCORTE, s[1]),
                             new Transition(s[1], FPCORTE, s[0]),
                             new Transition(s[1], ERROCORTE, s[0])
                         }, s[0], "SI22");//nome do automato

            interfaces.Add(SI22);

            Console.WriteLine("\tAutomaton: {0}", SI22.ToString());

            //Interface I23
            var SI23 = new DeterministicFiniteAutomaton(new[]
           {
                             new Transition(s[0], IPPIN, s[1]),
                             new Transition(s[1], FPPIN, s[0])
                         }, s[0], "SI23");//nome do automato

            interfaces.Add(SI23);

            Console.WriteLine("\tAutomaton: {0}", SI23.ToString());
        }

        private static void Verificationpoint34(DeterministicFiniteAutomaton S, DeterministicFiniteAutomaton I, int limit, int limit2, List<AbstractEvent> arlist)
        {
            var statesAndEventsList = S.DisabledEvents(I);
            int i = 0;
            int t = 0;
            //var last = pairStateEventList.Value.Count();
            foreach (var pairStateEventList in statesAndEventsList)
            {

                var last = pairStateEventList.Value.Count();

                if (limit2 == 1)
                {

                    foreach (var _event in pairStateEventList.Value)
                    {
                        if (arlist.Contains(_event))
                        {
                            if (t == 0)
                            {
                                Console.WriteLine("Disabled Events:");
                            }
                            Console.WriteLine("\tState: {0}", pairStateEventList.Key.ToString());
                            Console.WriteLine("\t\tEvent: {0}", _event.ToString());
                            t++;
                        }

                    }
                    if (i == limit - 1)
                    {
                        if (t != 0)
                        {
                            Console.Write("\tNot Pass");
                            Console.Write("\n");
                        }
                        if (t == 0)
                        {
                            Console.Write("\tPass");
                            Console.Write("\n");
                        }
                    }
                }
                if (limit2 == 2)
                {
                    foreach (var _event in pairStateEventList.Value)
                    {
                        if (arlist.Contains(_event))
                        {
                            if (t == 0)
                            {
                                Console.WriteLine("Disabled Events:");
                            }
                            Console.WriteLine("\tState: {0}", pairStateEventList.Key.ToString());
                            Console.WriteLine("\t\tEvent: {0}", _event.ToString());
                            t++;
                        }

                    }
                    if (i == limit - 1)
                    {
                        if (t != 0)
                        {
                            Console.Write("\tNot Pass");
                            Console.Write("\n");
                        }
                        if (t == 0)
                        {
                            Console.Write("\tPass");
                            Console.Write("\n");
                        }
                    }
                }

                if (++i >= limit) break;
            }
        }

        private static void Verificationpoint(DeterministicFiniteAutomaton supplanH, DeterministicFiniteAutomaton supplanL, DeterministicFiniteAutomaton interfaceL, List<AbstractEvent> requesteventsL, List<AbstractEvent> answereventsL)
        {
            Console.WriteLine("Point 3:");
            Console.WriteLine("\t");
            Verificationpoint34(supplanH, interfaceL, supplanH.States.Count(), 1, answereventsL);
            Console.WriteLine("Point 4:");
            Console.WriteLine("\t");
            Verificationpoint34(supplanL, interfaceL, supplanL.States.Count(), 2, requesteventsL);

            //////////////point 5 e 6 for Low/Intermediary Levels

            //declara variáveis necessárias para a verificação dos pontos 5 e 6
            Console.WriteLine("Point 5:");
            List<AbstractState> Yck_mkL = new List<AbstractState>();
            List<AbstractState> alcanstatesfndL = new List<AbstractState>();
            List<AbstractState> alcanstatespendL = new List<AbstractState>();
            List<AbstractState> nalcanstatesfndL = new List<AbstractState>();
            List<AbstractState> nalcanstatespendL = new List<AbstractState>();
            List<AbstractState> alcanstatesinterfaceL = new List<AbstractState>();
            List<AbstractState> YinYLxXmL = new List<AbstractState>();
            List<AbstractState> YL = new List<AbstractState>();
            List<AbstractState> YLL = new List<AbstractState>();
            List<AbstractEvent> answerfuncL = new List<AbstractEvent>();
            List<AbstractEvent> SigmanfndL = new List<AbstractEvent>();
            AbstractEvent pL, sigmaL, sigma1L;
            AbstractState yL, y1L, y2L, y3L, y4L;
            newsimplifyName(supplanL, out supplanL);
            var statessupplanL = supplanL.States.ToList();//YL
            // mstatessupplanL = supplanL.MarkedStates.ToList();//YLm
            var eventssupplanL = supplanL.Events.ToList();//SimgaG_L                    
            //var statesinterfaceL = interfaceL.States.ToList();//X
            var mstatesinterfaceL = interfaceL.MarkedStates.ToList();//Xm                                        
            var plantinterfaceL = supplanL.ParallelCompositionWith(interfaceL);//GIL
            var statesplantinterfaceL = plantinterfaceL.States.ToList();//YIL
            var mstatesplantinterfaceL = plantinterfaceL.MarkedStates.ToList();//YILm
            var eventsplantinterfaceL = plantinterfaceL.Events.ToList();//SigmaIL
            var spL = statessupplanL.Count();
            var imL = mstatesinterfaceL.Count();
            int gL = 0;
            //retira da lista criada os eventos não exclusivos da planta
            for (int i6 = 0; i6 < answereventsL.Count(); i6++)
            {
                eventssupplanL.Remove(answereventsL[i6]);
            }
            for (int i7 = 0; i7 < requesteventsL.Count(); i7++)
            {
                eventssupplanL.Remove(requesteventsL[i7]);
            }
            //plantinterfaceLdrawSVGFigure("plantinterfaceL", true);
            //plantinterfaceL.Trim.drawSVGFigure("plantinterfaceL", true);

            //Adciona os estados marcados na interface porem não marcados no nível inferior pertencentes a plantinterface21 em YinYLxXm21
            foreach (var splNiL in statesplantinterfaceL)
            {
                for (int c = 0; c < spL; c++)
                {
                    for (int i = 0; i < imL; i++)
                    {
                        if (splNiL.ToString() == String.Format("{0}|{1}", c, mstatesinterfaceL[i]))
                        {
                            YinYLxXmL.Add(splNiL);
                        }
                    }
                }
            }
            //Console.WriteLine("--------------------------------------------------------------------------");
            //foreach (var splm in mstatesplantinterfaceL)
            //{
            //    Console.WriteLine("marketstates:{0}", splm);
            //}
            //Console.WriteLine("--------------------------------------------------------------------------");

            // loop for para definir verificar todos os estados adicionados a YinYLxXmL
            for (int z = 0; z < YinYLxXmL.Count(); z++)
            {
                yL = YinYLxXmL[z];
                //Console.WriteLine("/tYinYLxXmL:{0}", yL);
                //adciona em Yck_mkL os estados de YinYLxXmL e elimina todos aqueles que são marcados na plantinterfaceL
                Yck_mkL.Add(yL);
                foreach (var splm in mstatesplantinterfaceL)
                {
                    if (Yck_mkL.Contains(splm) == true)
                    {
                        Yck_mkL.Remove(splm);
                        //Console.WriteLine("/Yck_mkL:{0}", yL);
                    }
                }
                //Console.WriteLine("--------------------------------------------------------------------------");

                //loop for para verificar cada evento de pergunta
                for (int i = 0; i < requesteventsL.Count(); i++)
                {
                    pL = requesteventsL[i];
                    // Cada estado alcançado em plantinterfaceL por meio desse evento de pergunta é adicionado em alcanstatesfndL e alcanstatespendL
                    foreach (var t in plantinterfaceL.Transitions)
                    {
                        if (pL == t.Trigger && yL == t.Origin)
                        {
                            //Console.WriteLine("/Yck_mkL:{0}", yL);
                            alcanstatesfndL.Add(t.Destination);
                            alcanstatespendL.Add(t.Destination);
                            /////// Answer(x), ou seja, adiciona as respostas possiveis a partir do evento de pergunta em questão 
                            foreach (var u1 in interfaceL.Transitions)
                            {
                                if (u1.Trigger == pL)
                                {
                                    alcanstatesinterfaceL.Add(u1.Destination);
                                }
                            }
                            for (int k = 0; k < alcanstatesinterfaceL.Count(); k++)
                            {
                                foreach (var u2 in interfaceL.Transitions)
                                {
                                    if (u2.Origin == alcanstatesinterfaceL[k])
                                    {
                                        answerfuncL.Add(u2.Trigger);
                                        SigmanfndL = answerfuncL;
                                    }
                                }
                            }

                            // verifica se a partir dos estados alcanstatespendL e SigmanfndL alcançam eventos de resposta 
                            while (alcanstatespendL.Count() != 0 && SigmanfndL.Count() != 0)
                            {
                                //pega o ultimo elemento de alcanstatespendL para análisar e o remove
                                y1L = alcanstatespendL[alcanstatespendL.Count() - 1];
                                alcanstatespendL.Remove(y1L);
                                //verifica se cada evento da plantinterfaceL esta contido em SigmanfndL e o removo caso estiver e incrementa, 
                                //com o estado alcançado por sigmaL,  alcanstatesfndL e alcanstatespendL caso o sigmaL seja um evento exclusivo 
                                //da planta e alcanstatesfndL não contenha esse estado  
                                for (int o = 0; o < eventsplantinterfaceL.Count(); o++)
                                {
                                    sigmaL = eventsplantinterfaceL[o];
                                    foreach (var s in plantinterfaceL.Transitions)
                                    {
                                        if (s.Origin == y1L)
                                        {
                                            if (s.Trigger == sigmaL)
                                            {
                                                y2L = s.Destination;
                                                if (SigmanfndL.Contains(sigmaL) == true)
                                                {
                                                    SigmanfndL.Remove(sigmaL);
                                                }
                                                if (eventssupplanL.Contains(sigmaL) == true && alcanstatesfndL.Contains(y2L) == false)
                                                {
                                                    alcanstatesfndL.Add(y2L);
                                                    alcanstatespendL.Add(y2L);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //verifica se o sistema alcançou todas as respostas se não ele não passou
                            if (SigmanfndL.Count() != 0)
                            {
                                if (gL == 0)
                                {
                                    Console.WriteLine("\t");
                                    Console.WriteLine("\tNot Pass");
                                    gL = 1;
                                }
                            }
                        }
                    }
                    alcanstatesfndL.Clear();
                    alcanstatespendL.Clear();
                    alcanstatesinterfaceL.Clear();
                    answerfuncL.Clear();

                }
            }

            if (gL == 0)
            {
                Console.WriteLine("\t");
                Console.WriteLine("\tPass");
            }

            // Console.WriteLine("--------------------------------------------------------------------------");

            Console.WriteLine("Point 6:");
            //Console.WriteLine("/tYck_mk count:{0}", Yck_mkL.Count);
            if (Yck_mkL.Count == 0)
            {
                Console.WriteLine("\t");
                Console.WriteLine("\tPass");
            }
            else
            {
                //for (int o2 = 0; o2 < Yck_mkL.Count(); o2++)
                //{
                //    Console.WriteLine("/tYck_mkL:{0}", Yck_mkL[o2]);
                //}            
                nalcanstatespendL = plantinterfaceL.MarkedStates.ToList();
                nalcanstatesfndL = plantinterfaceL.MarkedStates.ToList();
                int nL = 0;
                int bL = 0;

                while (nalcanstatespendL.Count() != 0)
                {
                    nL = nL + 1;
                    y3L = nalcanstatespendL[nalcanstatespendL.Count() - 1];
                    if (YLL.Contains(y3L) == false)
                    {
                        YLL.Add(y3L);
                    }
                    //Console.WriteLine("\ttest0:{0}", y3L);
                    //Console.WriteLine("\ttestcount1:{0}", nalcanstatespendL.Count());
                    nalcanstatespendL.Remove(y3L);
                    //Console.WriteLine("\ttestcount2:{0}", nalcanstatespendL.Count());                    
                    for (int i2 = 0; i2 < eventssupplanL.Count(); i2++)
                    {
                        sigma1L = eventssupplanL[i2];
                        foreach (var s5 in plantinterfaceL.Transitions)
                        {
                            if (s5.Destination == y3L)
                            {
                                if (s5.Trigger == sigma1L)
                                {
                                    if (YLL.Contains(s5.Origin) == false)
                                    {
                                        YL.Add(s5.Origin);
                                        YLL.Add(s5.Origin);
                                    }
                                }
                            }
                        }

                        if (YL.Count() != 0)
                        {
                            for (int i3 = 0; i3 < YL.Count(); i3++)
                            {

                                y4L = YL[i3];
                                //YLL.Add(y4L);

                                //Console.WriteLine("\ttest2:{0}", y4L);
                                //Console.WriteLine("\ttest3:{0}", YLL[YLL.Count() - 1]);
                                if (nalcanstatesfndL.Contains(y4L) == false)
                                {
                                    // Console.WriteLine("\ttest3:{0}", y4L);
                                    nalcanstatesfndL.Add(y4L);
                                    nalcanstatespendL.Add(y4L);
                                    if (Yck_mkL.Contains(y4L) == true)
                                    {
                                        Yck_mkL.Remove(y4L);
                                        //Console.WriteLine("\ttest4:{0}", Yck_mkL.Count());
                                    }
                                    if (Yck_mkL.Count() == 0 && bL == 0)
                                    {
                                        //Console.WriteLine("\t");
                                        Console.WriteLine("\tPass");
                                        bL = bL + 1;
                                    }
                                }
                            }
                        }
                        YL.Clear();
                    }
                    // Console.WriteLine("\ttest2");
                    if (bL != 0)
                    {
                        break;
                    }
                }
                //if (bL == 0 && Yck_mkL.Count() != 0)
                //{
                //    Console.WriteLine("\t Not Pass");
                //}
            }
        }

        private static void sintesesupHHISC(DeterministicFiniteAutomaton Plant1, DeterministicFiniteAutomaton Specification1, List<DeterministicFiniteAutomaton> interface1, List<AbstractEvent> answereventslist, List<int> numberanswer, out DeterministicFiniteAutomaton SFIM)
        {
            List<AbstractEvent> answerevents = new List<AbstractEvent>();
            List<AbstractState> failstates = new List<AbstractState>();
            List<AbstractEvent> answereventslistinicial = new List<AbstractEvent>();

            //newsimplifyName(Plant1, out Plant1);
            //Console.WriteLine("ok111");
            var interface1n = DeterministicFiniteAutomaton.ParallelComposition(interface1);
            newsimplifyName(interface1n, out interface1n);
            //Console.WriteLine("ok11");
            var Phigh = Plant1.ParallelCompositionWith(interface1n);
            //Console.WriteLine("ok1");
            var K11 = Plant1.ParallelCompositionWith(Specification1);
            var k1 = K11.ParallelCompositionWith(interface1);
            //Console.WriteLine("ok2");
            newsimplifyName(k1, out k1);
            k1 = k1.Trim;
            newsimplifyName(k1, out k1);
            // Console.WriteLine("K1 inicio: {0}", k1.States.Count());
            DeterministicFiniteAutomaton k1ini = k1;
            int l = 0;
            answereventslistinicial.AddRange(answereventslist);
            while (k1ini.States.Count() != k1.States.Count() || l == 0)
            {
                l = 1;
                k1ini = k1;
                answereventslist.AddRange(answereventslistinicial);
                //Console.WriteLine("answereventslistinicial while: {0}", answereventslistinicial.Count());
                //Console.WriteLine("answereventslist while: {0}", answereventslist.Count());

                for (int c = 0; c < numberanswer.Count(); c++)
                {
                    for (int i = 0; i < numberanswer[c]; i++)
                    {
                        answerevents.Add(answereventslist[0]);
                        answereventslist.Remove(answereventslist[0]);
                    }
                    //Point 3
                    var statesAndEventsList = k1.DisabledEvents(interface1[c]);
                    int h = 0;
                    int t = 0;
                    foreach (var pairStateEventList in statesAndEventsList)
                    {
                        foreach (var _event in pairStateEventList.Value)
                        {
                            if (answerevents.Contains(_event))
                            {
                                if (failstates.Contains(pairStateEventList.Key) == false)
                                {
                                    //Console.WriteLine("\tState: {0}", pairStateEventList.Key.ToString());
                                    failstates.Add(pairStateEventList.Key);
                                }
                                //Console.WriteLine("\t\tEvent: {0}", _event.ToString());
                                t++;
                            }
                        }
                        //if (h == k1.States.Count() - 1)
                        //{
                        //    if (t != 0)
                        //    {
                        //        Console.Write("\tNot Pass");
                        //        Console.Write("\n");
                        //        Console.Write("\tfailstates: {0}", failstates.Count());
                        //        Console.Write("\n");
                        //    }
                        //    if (t == 0)
                        //    {
                        //        Console.Write("\tPass");
                        //        Console.Write("\n");
                        //    }
                        //}
                        if (++h >= k1.States.Count()) break;
                    }

                    answerevents.Clear();
                    //failstates.Clear();
                }

                //Console.WriteLine("--------------------------------------------------------------------------");

                //Console.WriteLine("controláblidade Sigma u:");
                // Verificationcontrolability(k1, Plant1, k1.States.Count());
                int i2 = 0;
                int t2 = 0;
                //Console.WriteLine("UncontrollableEvents: {0}", G.UncontrollableEvents.Count());
                var statesAndEventsList2 = k1.DisabledEvents(Phigh);
                foreach (var pairStateEventList in statesAndEventsList2)
                {
                    foreach (var _event in pairStateEventList.Value)
                    {
                        if (Phigh.UncontrollableEvents.Contains(_event))
                        {
                            if (failstates.Contains(pairStateEventList.Key) == false)
                            {
                                //Console.WriteLine("\tState: {0}", pairStateEventList.Key.ToString());
                                failstates.Add(pairStateEventList.Key);
                            }
                            //Console.WriteLine("\t\tEvent: {0}", _event.ToString());
                            t2++;
                        }

                    }
                    //if (i2 == k1.States.Count() - 1)
                    //{
                    //    if (t2 != 0)
                    //    {
                    //        Console.Write("\tNot Pass");
                    //        Console.Write("\n");
                    //        Console.Write("\tfailstates: {0}", failstates.Count());
                    //        Console.Write("\n");
                    //    }
                    //    if (t2 == 0)
                    //    {
                    //        Console.Write("\tPass");
                    //        Console.Write("\n");
                    //    }
                    //}

                    if (++i2 >= k1.States.Count()) break;
                }
                //Console.WriteLine("--------------------------------------------------------------------------");


                List<AbstractEvent> EventosTransi = new List<AbstractEvent>(); //Os eventos que partem do estado em questão    
                List<AbstractState> EstadosAlcan = new List<AbstractState>();//Os estados alcançaveis a partir do estado em questão 
                List<AbstractState> OrdemEstadosAlcan = new List<AbstractState>();//Ordem correta dos estados alcançaveis
                List<AbstractState> OrdemEstadosAlcan2 = new List<AbstractState>();//Ordem da primeira aparição estados desde o começo
                List<AbstractState> OrdemEstadosAlcan3 = new List<AbstractState>();//Ordem correta dos estados alcançaveis desde o começo
                List<AbstractState> OrdemEstadosAlcanFim = new List<AbstractState>();//Ordem dos novos estados em cada iteração
                List<AbstractState> OrdemEstadosAlcanFim2 = new List<AbstractState>();//Ordem dos novos estados desde o começo
                List<int> FimEstados = new List<int>();

                var transG = new List<Transition>();
                var s = new List<State>();

                //lista com os nomes dos estados
                for (int u = 1; u < k1.States.Count(); u++)
                {
                    FimEstados.Add(u);
                }

                //Cria o estado inicial verificando sua marcação
                if (k1.MarkedStates.Contains(k1.InitialState))
                {
                    s.Add(new State(0.ToString(), Marking.Marked));
                }
                else
                {
                    s.Add(new State(0.ToString(), Marking.Unmarked));
                }

                OrdemEstadosAlcan2.Add(k1.InitialState);//adiciona o primeiro estado criado

                //iteração para criação dos novos estadose transições
                for (int c = 0; c < k1.States.Count(); c++)
                {
                    //verifica os eventos e estados alcançaveis pelo estado tratado
                    foreach (var t in k1.Transitions)
                    {
                        if (c == 0)
                        {
                            if (t.Origin == k1.InitialState)
                            {
                                EstadosAlcan.Add(t.Destination);
                                EventosTransi.Add(t.Trigger);
                            }
                        }
                        else
                        {
                            if (t.Origin == OrdemEstadosAlcan2[c])
                            {
                                EstadosAlcan.Add(t.Destination);
                                EventosTransi.Add(t.Trigger);
                            }
                        }
                    }

                    var OrdemEventosTransi = EventosTransi.OrderBy(n => n.ToString()).ToArray();//ordena os eventos 

                    //ordena os estados a partir dos eventos ordenandos e já adiciona em OrdemEstadosAlcan3
                    for (int n = 0; n < OrdemEventosTransi.Count(); n++)
                    {
                        for (int i = 0; i < EventosTransi.Count(); i++)
                        {
                            if (EventosTransi[i] == OrdemEventosTransi[n])
                            {
                                OrdemEstadosAlcan.Add(EstadosAlcan[i]);
                                OrdemEstadosAlcan3.Add(EstadosAlcan[i]);
                            }
                        }
                    }

                    //cria os novos estados e adiciona em OrdemEstadosAlcan2, OrdemEstadosAlcanFim e OrdemEstadosAlcanFim2
                    for (int h = 0; h < OrdemEstadosAlcan.Count(); h++)
                    {
                        //se for o estado inicial
                        if (OrdemEstadosAlcan[h] == k1.InitialState)
                        {
                            OrdemEstadosAlcanFim.Add(s[0]);
                            OrdemEstadosAlcanFim2.Add(s[0]);
                        }
                        else
                        {
                            //se OrdemEstadosAlcan2 não conter OrdemEstadosAlcan[h] quer dizer que pode ser criado um novo estado, 
                            //caso contrairio não é necessário, porém deve ser adicionado em uma nova tabela
                            if (OrdemEstadosAlcan2.Contains(OrdemEstadosAlcan[h]) == false)
                            {
                                if (k1.MarkedStates.Contains(OrdemEstadosAlcan[h]))
                                {
                                    s.Add(new State(FimEstados[0].ToString(), Marking.Marked));
                                    OrdemEstadosAlcanFim.Add(s[FimEstados[0]]);
                                    OrdemEstadosAlcanFim2.Add(s[FimEstados[0]]);
                                    FimEstados.Remove(FimEstados[0]);

                                }
                                else
                                {
                                    s.Add(new State(FimEstados[0].ToString(), Marking.Unmarked));
                                    OrdemEstadosAlcanFim.Add(s[FimEstados[0]]);
                                    OrdemEstadosAlcanFim2.Add(s[FimEstados[0]]);
                                    FimEstados.Remove(FimEstados[0]);
                                }
                                OrdemEstadosAlcan2.Add(OrdemEstadosAlcan[h]);
                            }
                            else
                            {
                                for (int j = 0; j < OrdemEstadosAlcan3.Count(); j++)
                                {

                                    if (OrdemEstadosAlcan[h] == OrdemEstadosAlcan3[j])
                                    {
                                        OrdemEstadosAlcanFim.Add(OrdemEstadosAlcanFim2[j]);
                                        OrdemEstadosAlcanFim2.Add(OrdemEstadosAlcanFim2[j]);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //cria as transições de acordo com as listas OrdemEventosTransi e OrdemEstadosAlcanFim
                    for (int y = 0; y < OrdemEventosTransi.Count(); y++)
                    {
                        if (c == 0)
                        {
                            if (failstates.Contains(s[0]) == false || failstates.Contains(OrdemEstadosAlcanFim[y]) == false)
                            {
                                transG.Add(new Transition(s[0], OrdemEventosTransi[y], OrdemEstadosAlcanFim[y]));
                            }
                        }
                        else
                        {
                            if (failstates.Contains(s[c]) == false || failstates.Contains(OrdemEstadosAlcanFim[y]) == false)
                            {
                                transG.Add(new Transition(s[c], OrdemEventosTransi[y], OrdemEstadosAlcanFim[y]));
                            }
                        }
                    }

                    OrdemEstadosAlcan.Clear();
                    EventosTransi.Clear();
                    OrdemEstadosAlcanFim.Clear();
                    EstadosAlcan.Clear();
                }

                //cria o automato novo
                k1 = new DeterministicFiniteAutomaton(transG, s[0], k1.Name.ToString());

                k1 = k1.Trim;
                newsimplifyName(k1, out k1);
                //Console.WriteLine("K1 fim: {0}", k1.States.Count());
                //Console.WriteLine("K1 transitions: {0}", k1.Transitions.Count());


                OrdemEstadosAlcanFim2.Clear();
                OrdemEstadosAlcan2.Clear();
                OrdemEstadosAlcan3.Clear();
                failstates.Clear();
            }

            SFIM = k1;
            //Console.WriteLine("SFIM fim: {0}", SFIM.States.Count());
            //Console.WriteLine("SFIM transitions: {0}", SFIM.Transitions.Count());
        }

        private static void sintesesupLHISC(DeterministicFiniteAutomaton Plant21, DeterministicFiniteAutomaton Specification21, DeterministicFiniteAutomaton interface21, List<AbstractEvent> requestevents21, List<AbstractEvent> answerevents21, out DeterministicFiniteAutomaton SFIM)
        {
            List<AbstractState> failstates = new List<AbstractState>();
            List<AbstractState> failstatesp6 = new List<AbstractState>();

            //newsimplifyName(Plant21, out Plant21);
            //var Shigh = Specification21.ParallelCompositionWith(interface21);
            //Console.WriteLine("ok12");
            var K211 = Plant21.ParallelCompositionWith(Specification21);
            //Console.WriteLine("ok1");
            var k21 = K211.ParallelCompositionWith(interface21);
            //Console.WriteLine("ok2");
            //Console.WriteLine("K1 inicio 1: {0}", k21.States.Count());
            //Console.WriteLine("K1 inicio 1: {0}", k21.Transitions.Count());
            newsimplifyName(k21, out k21);
            //Console.WriteLine("ok25");
            k21 = k21.Trim;
            //Console.WriteLine("ok3");
            newsimplifyName(k21, out k21);
            //Console.WriteLine("K1 inicio: {0}", k21.States.Count());
            DeterministicFiniteAutomaton k21ini = k21;
            int l1 = 0;
            while (k21ini.States.Count() != k21.States.Count() || l1 == 0)
            {
                l1 = 1;
                k21ini = k21;

                //Console.WriteLine("Point 4:");
                //Console.WriteLine("\t");
                var statesAndEventsList = k21.DisabledEvents(interface21);
                int i1 = 0;
                int t1 = 0;
                //var last = pairStateEventList.Value.Count();
                foreach (var pairStateEventList in statesAndEventsList)
                {
                    //var last = pairStateEventList.Value.Count();
                    foreach (var _event in pairStateEventList.Value)
                    {
                        if (requestevents21.Contains(_event))
                        {
                            if (failstates.Contains(pairStateEventList.Key) == false)
                            {
                                //Console.WriteLine("\tState: {0}", pairStateEventList.Key.ToString());
                                failstates.Add(pairStateEventList.Key);
                            }
                            //Console.WriteLine("\t\tEvent: {0}", _event.ToString());
                            t1++;
                        }
                    }
                    //if (i1 == k21.States.Count() - 1)
                    //{
                    //    if (t1 != 0)
                    //    {
                    //        Console.Write("\tNot Pass");
                    //        Console.Write("\n");
                    //        Console.Write("\tfailstates: {0}", failstates.Count());
                    //        Console.Write("\n");
                    //    }
                    //    if (t1 == 0)
                    //    {
                    //        Console.Write("\tPass");
                    //        Console.Write("\n");
                    //    }
                    //}
                    if (++i1 >= k21.States.Count()) break;
                }


                //Console.WriteLine("--------------------------------------------------------------------------");

                //Console.WriteLine("controláblidade Sigma u:");
                // Verificationcontrolability(k1, Plant1, k1.States.Count());
                int i2 = 0;
                int t2 = 0;
                //Console.WriteLine("UncontrollableEvents: {0}", G.UncontrollableEvents.Count());
                //var last = pairStateEventList.Value.Count();
                var statesAndEventsList2 = k21.DisabledEvents(Plant21);
                foreach (var pairStateEventList in statesAndEventsList2)
                {
                    //var last = pairStateEventList.Value.Count();
                    foreach (var _event in pairStateEventList.Value)
                    {
                        if (Plant21.UncontrollableEvents.Contains(_event))
                        {
                            if (failstates.Contains(pairStateEventList.Key) == false)
                            {
                                //Console.WriteLine("\tState: {0}", pairStateEventList.Key.ToString());
                                failstates.Add(pairStateEventList.Key);
                            }
                            //Console.WriteLine("\t\tEvent: {0}", _event.ToString());
                            t2++;
                        }

                    }
                    //if (i2 == k21.States.Count() - 1)
                    //{
                    //    if (t2 != 0)
                    //    {
                    //        Console.Write("\tNot Pass");
                    //        Console.Write("\n");
                    //        Console.Write("\tfailstates: {0}", failstates.Count());
                    //        Console.Write("\n");
                    //    }
                    //    if (t2 == 0)
                    //    {
                    //        Console.Write("\tPass");
                    //        Console.Write("\n");
                    //    }
                    //}

                    if (++i2 >= k21.States.Count()) break;
                }
                //Console.WriteLine("--------------------------------------------------------------------------");

                //declara variáveis necessárias para a verificação dos pontos 5 e 6
                //Console.WriteLine("Point 5:");
                List<AbstractState> Yck_mk21 = new List<AbstractState>();
                List<AbstractState> alcanstatesfnd21 = new List<AbstractState>();
                List<AbstractState> alcanstatespend21 = new List<AbstractState>();
                List<AbstractState> nalcanstatesfnd21 = new List<AbstractState>();
                List<AbstractState> nalcanstatespend21 = new List<AbstractState>();
                List<AbstractState> alcanstatesinterface21 = new List<AbstractState>();
                List<AbstractState> alcanstatesinterface22 = new List<AbstractState>();
                List<AbstractState> alcanstatesinterface23 = new List<AbstractState>();

                List<AbstractState> YinYLxXm21 = new List<AbstractState>();
                List<AbstractState> Y21 = new List<AbstractState>();
                List<AbstractState> Y221 = new List<AbstractState>();
                List<AbstractState> Y2221 = new List<AbstractState>();
                List<AbstractEvent> answerfunc21 = new List<AbstractEvent>();
                List<AbstractEvent> requestfunc21 = new List<AbstractEvent>();
                List<AbstractEvent> Sigmanfnd21 = new List<AbstractEvent>();
                List<int> failstatesplant = new List<int>();

                AbstractEvent p21, sigma21, sigma121;
                AbstractState y21, y121, y221, y321, y421;
                newsimplifyName(k21, out k21);
                var statesplant21 = k21.States.ToList();//YL
                var mstatesplant21 = k21.MarkedStates.ToList();//YLm
                var eventsplant21 = k21.Events.ToList();//SimgaG_L                    
                var statesinterface21 = interface21.States.ToList();//X
                var mstatesinterface21 = interface21.MarkedStates.ToList();//Xm                                        
                var plantinterface21 = k21.ParallelCompositionWith(interface21);//GIL
                var statesplantinterface21 = plantinterface21.States.ToList();//YIL
                var mstatesplantinterface21 = plantinterface21.MarkedStates.ToList();//YILm
                var eventsplantinterface21 = plantinterface21.Events.ToList();//SigmaIL
                var sp21 = statesplant21.Count();
                var im21 = mstatesinterface21.Count();
                int g21 = 0;
                //retira da lista criada os eventos não exclusivos da planta
                for (int i6 = 0; i6 < answerevents21.Count(); i6++)
                {
                    eventsplant21.Remove(answerevents21[i6]);
                }
                for (int i7 = 0; i7 < requestevents21.Count(); i7++)
                {
                    eventsplant21.Remove(requestevents21[i7]);
                }
                //plantinterface21.drawSVGFigure("plantinterface21", true);
                //plantinterface21.Trim.drawSVGFigure("plantinterface21trim", true);

                //Adciona os estados marcados na interface porem não marcados no nível inferior pertencentes a plantinterface21 em YinYLxXm21
                foreach (var splNi21 in statesplantinterface21)
                {
                    for (int c = 0; c < sp21; c++)
                    {
                        for (int i = 0; i < im21; i++)
                        {
                            if (splNi21.ToString() == String.Format("{0}|{1}", c, mstatesinterface21[i]))
                            {
                                YinYLxXm21.Add(splNi21);
                            }
                        }
                    }
                }
                //Console.WriteLine("--------------------------------------------------------------------------");
                //foreach (var splm in mstatesplantinterface21)
                //{

                //    Console.WriteLine("marketstates:{0}", splm);
                //}
                //Console.WriteLine("--------------------------------------------------------------------------");

                // loop for para definir verificar todos os estados adicionados a YinYLxXm21
                for (int z = 0; z < YinYLxXm21.Count(); z++)
                {
                    y21 = YinYLxXm21[z];
                    //Console.WriteLine("/tYinYLxXm21:{0}", y21);
                    //adciona em Yck_mk21 os estados de YinYLxXm21 e elimina todos aqueles que são marcados na plantinterface21
                    Yck_mk21.Add(y21);
                    foreach (var splm in mstatesplantinterface21)
                    {
                        if (Yck_mk21.Contains(splm) == true)
                        {
                            Yck_mk21.Remove(splm);
                            //Console.WriteLine("/Yck_mk21:{0}", y21);
                        }
                    }
                    //Console.WriteLine("--------------------------------------------------------------------------");

                    //loop for para verificar cada evento de pergunta
                    for (int i = 0; i < requestevents21.Count(); i++)
                    {
                        p21 = requestevents21[i];
                        // Cada estado alcançado em plantinterface21 por meio desse evento de pergunta é adicionado em alcanstatesfnd21 e alcanstatespend21
                        foreach (var t in plantinterface21.Transitions)
                        {
                            if (p21 == t.Trigger && y21 == t.Origin)
                            {
                                //Console.WriteLine("/Yck_mk21:{0}", y21);
                                alcanstatesfnd21.Add(t.Destination);
                                alcanstatespend21.Add(t.Destination);

                                /////// Answer(x), ou seja, adiciona as respostas possiveis a partir do evento de pergunta em questão 
                                foreach (var u1 in interface21.Transitions)
                                {
                                    if (u1.Trigger == p21)
                                    {
                                        alcanstatesinterface21.Add(u1.Destination);
                                    }
                                }
                                for (int k = 0; k < alcanstatesinterface21.Count(); k++)
                                {
                                    foreach (var u2 in interface21.Transitions)
                                    {
                                        if (u2.Origin == alcanstatesinterface21[k])
                                        {
                                            answerfunc21.Add(u2.Trigger);
                                            Sigmanfnd21 = answerfunc21;
                                            alcanstatesinterface22.Add(t.Destination);
                                        }
                                    }
                                }

                                // verifica se a partir dos estados alcanstatespend21 e Sigmanfnd21 alcançam eventos de resposta 
                                while (alcanstatespend21.Count() != 0 && Sigmanfnd21.Count() != 0)
                                {
                                    //pega o ultimo elemento de alcanstatespend21 para análisar e o remove
                                    y121 = alcanstatespend21[alcanstatespend21.Count() - 1];
                                    alcanstatespend21.Remove(y121);
                                    if (alcanstatesinterface22.Contains(y121))
                                    {
                                        for (int o21 = 0; o21 < alcanstatesinterface22.Count(); o21++)
                                        {
                                            if (alcanstatesinterface22[o21] == y121)
                                            {
                                                alcanstatesinterface23.Add(y121);
                                            }
                                        }
                                    }
                                    //verifica se cada evento da plantinterface21 esta contido em Sigmanfnd21 e o removo caso estiver e incrementa, 
                                    //com o estado alcançado por sigma21,  alcanstatesfnd21 e alcanstatespend21 caso o sigma21 seja um evento exclusivo 
                                    //da planta e alcanstatesfnd21 não contenha esse estado  
                                    for (int o = 0; o < eventsplantinterface21.Count(); o++)
                                    {
                                        sigma21 = eventsplantinterface21[o];
                                        foreach (var s21 in plantinterface21.Transitions)
                                        {
                                            if (s21.Origin == y121)
                                            {
                                                if (s21.Trigger == sigma21)
                                                {
                                                    y221 = s21.Destination;
                                                    if (Sigmanfnd21.Contains(sigma21) == true)
                                                    {
                                                        Sigmanfnd21.Remove(sigma21);
                                                        alcanstatesinterface23.Remove(alcanstatesinterface23[alcanstatesinterface23.Count() - 1]);
                                                    }
                                                    if (eventsplant21.Contains(sigma21) == true && alcanstatesfnd21.Contains(y221) == false)
                                                    {
                                                        alcanstatesfnd21.Add(y221);
                                                        alcanstatespend21.Add(y221);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //verifica se o sistema alcançou todas as respostas se não ele não passou
                                if (Sigmanfnd21.Count() != 0)
                                {
                                    //pega os estados que falham na planta||interface e tranforma só pra "planta" 
                                    foreach (var statesplantintver in alcanstatesinterface23)
                                    {
                                        for (int c21 = 0; c21 < sp21; c21++)
                                        {
                                            for (int i21 = 0; i21 < im21; i21++)
                                            {
                                                if (statesplantintver.ToString() == String.Format("{0}|{1}", c21, mstatesinterface21[i21]))
                                                {
                                                    failstatesplant.Add(c21);
                                                }
                                            }
                                        }
                                    }
                                    foreach (var statesplantver in statesplant21)
                                    {
                                        for (int c21 = 0; c21 < failstatesplant.Count(); c21++)
                                        {
                                            if (statesplantver.ToString() == String.Format("{0}", failstatesplant[c21]))
                                            {
                                                if (failstates.Contains(statesplantver) == false)
                                                {
                                                    failstates.Add(statesplantver);
                                                }
                                            }
                                        }
                                    }

                                    //Console.WriteLine("\t");
                                    //Console.WriteLine("\tNot Pass");
                                    g21 = 1;
                                }
                                alcanstatesfnd21.Clear();
                                alcanstatespend21.Clear();
                                alcanstatesinterface22.Clear();
                                alcanstatesinterface23.Clear();
                                alcanstatesinterface21.Clear();
                                failstatesplant.Clear();
                                answerfunc21.Clear();
                            }
                        }


                    }
                }

                //if (g21 == 0)
                //{
                //    Console.WriteLine("\t");
                //    Console.WriteLine("\tPass");
                //}
                // Console.WriteLine("--------------------------------------------------------------------------");

                //Console.WriteLine("Point 6:");
                //Console.WriteLine("/tYck_mk count:{0}", Yck_mk21.Count);
                //verifica se existem estados não marcados na planta e marcados na interface
                if (Yck_mk21.Count == 0)
                {
                    //Console.WriteLine("\t");
                    //Console.WriteLine("\tPass");
                }
                else
                {
                    //for (int o2 = 0; o2 < Yck_mk21.Count(); o2++)
                    //{
                    //    Console.WriteLine("/tYck_mk21:{0}", Yck_mk21[o2]);
                    //} 
                    //declaração de variaveis
                    failstatesp6.AddRange(Yck_mk21);
                    nalcanstatespend21 = plantinterface21.MarkedStates.ToList();
                    nalcanstatesfnd21 = plantinterface21.MarkedStates.ToList();
                    int n21 = 0;
                    int b21 = 0;

                    //verificação ciclica ponto 6
                    while (nalcanstatespend21.Count() != 0)
                    {
                        n21 = n21 + 1;
                        //retira o ultimo elemento da lista em questão
                        y321 = nalcanstatespend21[nalcanstatespend21.Count() - 1];
                        //adiciona em Y2221 apenas estados nunca visitados
                        if (Y2221.Contains(y321) == false)
                        {
                            Y2221.Add(y321);
                        }
                        //Console.WriteLine("\ttest0:{0}", y321);
                        //Console.WriteLine("\ttestcount1:{0}", nalcanstatespend21.Count());
                        nalcanstatespend21.Remove(y321);
                        //Console.WriteLine("\ttestcount2:{0}", nalcanstatespend21.Count());   
                        //seguindo apenas eventos exclusivos da planta
                        for (int i21 = 0; i21 < eventsplant21.Count(); i21++)
                        {
                            sigma121 = eventsplant21[i21];
                            //anda inversamente pela plantainterface apartir de estados marcados por meio de eventos exclusivos a planta
                            foreach (var s5 in plantinterface21.Transitions)
                            {
                                if (s5.Destination == y321)
                                {
                                    if (s5.Trigger == sigma121)
                                    {
                                        //adiciona um novo estado a y21 e y2221 caso y2221 não contenha o novo estado
                                        if (Y2221.Contains(s5.Origin) == false)
                                        {
                                            Y21.Add(s5.Origin);
                                            Y2221.Add(s5.Origin);
                                        }
                                    }
                                }
                            }
                            //caso y21 seja diferente de zero ou seja o sistema alcançou novos estados
                            if (Y21.Count() != 0)
                            {
                                //analisa cada estado alcançado
                                for (int i3 = 0; i3 < Y21.Count(); i3++)
                                {

                                    y421 = Y21[i3];
                                    //Y2221.Add(y421);

                                    //Console.WriteLine("\ttest2:{0}", y421);
                                    //Console.WriteLine("\ttest3:{0}", Y2221[Y2221.Count() - 1]);
                                    //se o estado ainda não foi visitado
                                    if (nalcanstatesfnd21.Contains(y421) == false)
                                    {
                                        // Console.WriteLine("\ttest3:{0}", y421);
                                        //adiciona em nalcanstatesfnd21 e nalcanstatespend21
                                        nalcanstatesfnd21.Add(y421);
                                        nalcanstatespend21.Add(y421);
                                        if (Yck_mk21.Contains(y421) == true)
                                        {
                                            Yck_mk21.Remove(y421);
                                            failstatesp6.Remove(y421);
                                            //Console.WriteLine("\ttest4:{0}", Yck_mk21.Count());
                                        }
                                        if (Yck_mk21.Count() == 0 && b21 == 0)
                                        {
                                            //Console.WriteLine("\t");
                                            //Console.WriteLine("\tPass");
                                            b21 = b21 + 1;
                                        }
                                    }
                                }
                            }
                            Y21.Clear();
                        }
                        // Console.WriteLine("\ttest2");
                        if (b21 != 0)
                        {
                            break;
                        }
                    }
                    if (nalcanstatespend21.Count() == 0 && b21 == 0)
                    {
                        //Console.WriteLine("\t");
                        //Console.WriteLine("\tNot Pass");
                        failstates.AddRange(failstatesp6);
                    }
                }

                //Console.WriteLine("--------------------------------------------------------------------------");

                List<AbstractEvent> EventosTransi = new List<AbstractEvent>(); //Os eventos que partem do estado em questão    
                List<AbstractState> EstadosAlcan = new List<AbstractState>();//Os estados alcançaveis a partir do estado em questão 
                List<AbstractState> OrdemEstadosAlcan = new List<AbstractState>();//Ordem correta dos estados alcançaveis
                List<AbstractState> OrdemEstadosAlcan2 = new List<AbstractState>();//Ordem da primeira aparição estados desde o começo
                List<AbstractState> OrdemEstadosAlcan3 = new List<AbstractState>();//Ordem correta dos estados alcançaveis desde o começo
                List<AbstractState> OrdemEstadosAlcanFim = new List<AbstractState>();//Ordem dos novos estados em cada iteração
                List<AbstractState> OrdemEstadosAlcanFim2 = new List<AbstractState>();//Ordem dos novos estados desde o começo
                List<int> FimEstados = new List<int>();

                var transG = new List<Transition>();
                var s = new List<State>();

                //lista com os nomes dos estados
                for (int u = 1; u < k21.States.Count(); u++)
                {
                    FimEstados.Add(u);
                }

                //Cria o estado inicial verificando sua marcação
                if (k21.MarkedStates.Contains(k21.InitialState))
                {
                    s.Add(new State(0.ToString(), Marking.Marked));
                }
                else
                {
                    s.Add(new State(0.ToString(), Marking.Unmarked));
                }

                OrdemEstadosAlcan2.Add(k21.InitialState);//adiciona o primeiro estado criado

                //iteração para criação dos novos estadose transições
                for (int c = 0; c < k21.States.Count(); c++)
                {
                    //verifica os eventos e estados alcançaveis pelo estado tratado
                    foreach (var t in k21.Transitions)
                    {
                        if (c == 0)
                        {
                            if (t.Origin == k21.InitialState)
                            {
                                EstadosAlcan.Add(t.Destination);
                                EventosTransi.Add(t.Trigger);
                            }
                        }
                        else
                        {
                            if (t.Origin == OrdemEstadosAlcan2[c])
                            {
                                EstadosAlcan.Add(t.Destination);
                                EventosTransi.Add(t.Trigger);
                            }
                        }
                    }

                    var OrdemEventosTransi = EventosTransi.OrderBy(n => n.ToString()).ToArray();//ordena os eventos 

                    //ordena os estados a partir dos eventos ordenandos e já adiciona em OrdemEstadosAlcan3
                    for (int n = 0; n < OrdemEventosTransi.Count(); n++)
                    {
                        for (int i = 0; i < EventosTransi.Count(); i++)
                        {
                            if (EventosTransi[i] == OrdemEventosTransi[n])
                            {
                                OrdemEstadosAlcan.Add(EstadosAlcan[i]);
                                OrdemEstadosAlcan3.Add(EstadosAlcan[i]);
                            }
                        }
                    }

                    //cria os novos estados e adiciona em OrdemEstadosAlcan2, OrdemEstadosAlcanFim e OrdemEstadosAlcanFim2
                    for (int h = 0; h < OrdemEstadosAlcan.Count(); h++)
                    {
                        //se for o estado inicial
                        if (OrdemEstadosAlcan[h] == k21.InitialState)
                        {
                            OrdemEstadosAlcanFim.Add(s[0]);
                            OrdemEstadosAlcanFim2.Add(s[0]);
                        }
                        else
                        {
                            //se OrdemEstadosAlcan2 não conter OrdemEstadosAlcan[h] quer dizer que pode ser criado um novo estado, 
                            //caso contrairio não é necessário, porém deve ser adicionado em uma nova tabela
                            if (OrdemEstadosAlcan2.Contains(OrdemEstadosAlcan[h]) == false)
                            {
                                if (k21.MarkedStates.Contains(OrdemEstadosAlcan[h]))
                                {
                                    s.Add(new State(FimEstados[0].ToString(), Marking.Marked));
                                    OrdemEstadosAlcanFim.Add(s[FimEstados[0]]);
                                    OrdemEstadosAlcanFim2.Add(s[FimEstados[0]]);
                                    FimEstados.Remove(FimEstados[0]);

                                }
                                else
                                {
                                    s.Add(new State(FimEstados[0].ToString(), Marking.Unmarked));
                                    OrdemEstadosAlcanFim.Add(s[FimEstados[0]]);
                                    OrdemEstadosAlcanFim2.Add(s[FimEstados[0]]);
                                    FimEstados.Remove(FimEstados[0]);
                                }
                                OrdemEstadosAlcan2.Add(OrdemEstadosAlcan[h]);
                            }
                            else
                            {
                                for (int j = 0; j < OrdemEstadosAlcan3.Count(); j++)
                                {

                                    if (OrdemEstadosAlcan[h] == OrdemEstadosAlcan3[j])
                                    {
                                        OrdemEstadosAlcanFim.Add(OrdemEstadosAlcanFim2[j]);
                                        OrdemEstadosAlcanFim2.Add(OrdemEstadosAlcanFim2[j]);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //cria as transições de acordo com as listas OrdemEventosTransi e OrdemEstadosAlcanFim
                    for (int y = 0; y < OrdemEventosTransi.Count(); y++)
                    {
                        if (c == 0)
                        {
                            if (failstates.Contains(s[0]) == false || failstates.Contains(OrdemEstadosAlcanFim[y]) == false)
                            {
                                transG.Add(new Transition(s[0], OrdemEventosTransi[y], OrdemEstadosAlcanFim[y]));
                            }
                        }
                        else
                        {
                            if (failstates.Contains(s[c]) == false || failstates.Contains(OrdemEstadosAlcanFim[y]) == false)
                            {
                                transG.Add(new Transition(s[c], OrdemEventosTransi[y], OrdemEstadosAlcanFim[y]));
                            }
                        }
                    }

                    OrdemEstadosAlcan.Clear();
                    EventosTransi.Clear();
                    OrdemEstadosAlcanFim.Clear();
                    EstadosAlcan.Clear();
                }

                //cria o automato novo
                k21 = new DeterministicFiniteAutomaton(transG, s[0], k21.Name.ToString());

                k21 = k21.Trim;
                newsimplifyName(k21, out k21);
                //Console.WriteLine("K1 fim: {0}", k21.States.Count());


                OrdemEstadosAlcanFim2.Clear();
                OrdemEstadosAlcan2.Clear();
                OrdemEstadosAlcan3.Clear();
                failstates.Clear();
            }

            SFIM = k21;
        }

        private static void sintesesupIHISC(DeterministicFiniteAutomaton Plant21, DeterministicFiniteAutomaton Specification21, DeterministicFiniteAutomaton interface21, List<AbstractEvent> requestevents21, List<AbstractEvent> answerevents21, List<DeterministicFiniteAutomaton> interface1, List<AbstractEvent> answereventslist, List<int> numberanswer, out DeterministicFiniteAutomaton SFIM)
        {

            //////////////////////////////////////////////////////////////////////
            List<AbstractState> failstates = new List<AbstractState>();
            List<AbstractState> failstatesp6 = new List<AbstractState>();
            List<AbstractEvent> answerevents = new List<AbstractEvent>();
            List<AbstractEvent> answereventslistinicial = new List<AbstractEvent>();

            //newsimplifyName(Plant21, out Plant21);
            var Shigh = Specification21.ParallelCompositionWith(interface21);
            var interface1n = DeterministicFiniteAutomaton.ParallelComposition(interface1);
            var Phigh = Plant21.ParallelCompositionWith(interface1n);
            var K211 = Plant21.ParallelCompositionWith(Specification21);
            var k21 = K211.ParallelCompositionWith(interface21, interface1n);
            newsimplifyName(k21, out k21);
            k21 = k21.Trim;
            newsimplifyName(k21, out k21);
            //Console.WriteLine("K1 inicio: {0}", k21.States.Count());
            DeterministicFiniteAutomaton k21ini = k21;
            int l1 = 0;
            answereventslistinicial.AddRange(answereventslist);
            while (k21ini.States.Count() != k21.States.Count() || l1 == 0)
            {
                l1 = 1;
                k21ini = k21;
                answereventslist.AddRange(answereventslistinicial);
                //Console.WriteLine("answereventslistinicial while: {0}", answereventslistinicial.Count());
                //Console.WriteLine("answereventslist while: {0}", answereventslist.Count());

                ////point 3/////////////////////////////////////////////////////////////////////////////////////
                for (int c = 0; c < numberanswer.Count(); c++)
                {
                    for (int i = 0; i < numberanswer[c]; i++)
                    {
                        answerevents.Add(answereventslist[0]);
                        answereventslist.Remove(answereventslist[0]);
                    }
                    //Console.WriteLine("Point 3:");
                    //Console.WriteLine("\t");
                    var statesAndEventsListH = k21.DisabledEvents(interface1[c]);
                    int h = 0;
                    int t = 0;
                    foreach (var pairStateEventList in statesAndEventsListH)
                    {
                        foreach (var _event in pairStateEventList.Value)
                        {
                            if (answerevents.Contains(_event))
                            {
                                if (failstates.Contains(pairStateEventList.Key) == false)
                                {
                                    //Console.WriteLine("\tState: {0}", pairStateEventList.Key.ToString());
                                    failstates.Add(pairStateEventList.Key);
                                }
                                //Console.WriteLine("\t\tEvent: {0}", _event.ToString());
                                t++;
                            }
                        }
                        //if (h == k1.States.Count() - 1)
                        //{
                        //   if (t != 0)
                        //   {
                        //   Console.Write("\tNot Pass");
                        //   Console.Write("\n");
                        //   Console.Write("\tfailstates: {0}", failstates.Count());
                        //   Console.Write("\n");
                        //   }
                        //   if (t == 0)
                        //   {
                        //   Console.Write("\tPass");
                        //   Console.Write("\n");
                        //   }
                        //}
                        if (++h >= k21.States.Count()) break;
                    }

                    answerevents.Clear();
                    //failstates.Clear();
                }

                ////point 4/////////////////////////////////////////////////////////////////////////////////////

                //Console.WriteLine("Point 4:");
                //Console.WriteLine("\t");
                var statesAndEventsList = k21.DisabledEvents(interface21);
                int i1 = 0;
                int t1 = 0;
                //var last = pairStateEventList.Value.Count();
                foreach (var pairStateEventList in statesAndEventsList)
                {
                    //var last = pairStateEventList.Value.Count();
                    foreach (var _event in pairStateEventList.Value)
                    {
                        if (requestevents21.Contains(_event))
                        {
                            if (failstates.Contains(pairStateEventList.Key) == false)
                            {
                                //Console.WriteLine("\tState: {0}", pairStateEventList.Key.ToString());
                                failstates.Add(pairStateEventList.Key);
                            }
                            //Console.WriteLine("\t\tEvent: {0}", _event.ToString());
                            t1++;
                        }
                    }
                    //if (i1 == k21.States.Count() - 1)
                    //{
                    //    if (t1 != 0)
                    //    {
                    //        Console.Write("\tNot Pass");
                    //        Console.Write("\n");
                    //        Console.Write("\tfailstates: {0}", failstates.Count());
                    //        Console.Write("\n");
                    //    }
                    //    if (t1 == 0)
                    //    {
                    //        Console.Write("\tPass");
                    //        Console.Write("\n");
                    //    }
                    //}
                    if (++i1 >= k21.States.Count()) break;
                }

                ////Sigma u Controllability://///////////////////////////////////////////////////////////////////////////////////

                //Console.WriteLine("Sigma u Controllability:");
                // Verificationcontrolability(k1, Plant1, k1.States.Count());
                int i2 = 0;
                int t2 = 0;
                //Console.WriteLine("UncontrollableEvents: {0}", G.UncontrollableEvents.Count());
                //var last = pairStateEventList.Value.Count();
                var statesAndEventsList2 = k21.DisabledEvents(Phigh);
                foreach (var pairStateEventList in statesAndEventsList2)
                {
                    //var last = pairStateEventList.Value.Count();
                    foreach (var _event in pairStateEventList.Value)
                    {
                        if (Phigh.UncontrollableEvents.Contains(_event))
                        {
                            if (failstates.Contains(pairStateEventList.Key) == false)
                            {
                                //Console.WriteLine("\tState: {0}", pairStateEventList.Key.ToString());
                                failstates.Add(pairStateEventList.Key);
                            }
                            //Console.WriteLine("\t\tEvent: {0}", _event.ToString());
                            t2++;
                        }

                    }
                    //if (i2 == k21.States.Count() - 1)
                    //{
                    //    if (t2 != 0)
                    //    {
                    //        Console.Write("\tNot Pass");
                    //        Console.Write("\n");
                    //        Console.Write("\tfailstates: {0}", failstates.Count());
                    //        Console.Write("\n");
                    //    }
                    //    if (t2 == 0)
                    //    {
                    //        Console.Write("\tPass");
                    //        Console.Write("\n");
                    //    }
                    //}

                    if (++i2 >= k21.States.Count()) break;
                }
                //Console.WriteLine("--------------------------------------------------------------------------");

                //declara variáveis necessárias para a verificação dos pontos 5 e 6
                //Console.WriteLine("Point 5:");
                List<AbstractState> Yck_mk21 = new List<AbstractState>();
                List<AbstractState> alcanstatesfnd21 = new List<AbstractState>();
                List<AbstractState> alcanstatespend21 = new List<AbstractState>();
                List<AbstractState> nalcanstatesfnd21 = new List<AbstractState>();
                List<AbstractState> nalcanstatespend21 = new List<AbstractState>();
                List<AbstractState> alcanstatesinterface21 = new List<AbstractState>();
                List<AbstractState> alcanstatesinterface22 = new List<AbstractState>();
                List<AbstractState> alcanstatesinterface23 = new List<AbstractState>();

                List<AbstractState> YinYLxXm21 = new List<AbstractState>();
                List<AbstractState> Y21 = new List<AbstractState>();
                List<AbstractState> Y221 = new List<AbstractState>();
                List<AbstractState> Y2221 = new List<AbstractState>();
                List<AbstractEvent> answerfunc21 = new List<AbstractEvent>();
                List<AbstractEvent> requestfunc21 = new List<AbstractEvent>();
                List<AbstractEvent> Sigmanfnd21 = new List<AbstractEvent>();
                List<int> failstatesplant = new List<int>();

                AbstractEvent p21, sigma21, sigma121;
                AbstractState y21, y121, y221, y321, y421;
                newsimplifyName(k21, out k21);
                var statesplant21 = k21.States.ToList();//YL
                var mstatesplant21 = k21.MarkedStates.ToList();//YLm
                var eventsplant21 = k21.Events.ToList();//SimgaG_L                    
                var statesinterface21 = interface21.States.ToList();//X
                var mstatesinterface21 = interface21.MarkedStates.ToList();//Xm                                        
                var plantinterface21 = k21.ParallelCompositionWith(interface21);//GIL
                var statesplantinterface21 = plantinterface21.States.ToList();//YIL
                var mstatesplantinterface21 = plantinterface21.MarkedStates.ToList();//YILm
                var eventsplantinterface21 = plantinterface21.Events.ToList();//SigmaIL
                var sp21 = statesplant21.Count();
                var im21 = mstatesinterface21.Count();
                int g21 = 0;
                //retira da lista criada os eventos não exclusivos da planta
                for (int i6 = 0; i6 < answerevents21.Count(); i6++)
                {
                    eventsplant21.Remove(answerevents21[i6]);
                }
                for (int i7 = 0; i7 < requestevents21.Count(); i7++)
                {
                    eventsplant21.Remove(requestevents21[i7]);
                }
                //plantinterface21.drawSVGFigure("plantinterface21", true);
                //plantinterface21.Trim.drawSVGFigure("plantinterface21trim", true);

                //Adciona os estados marcados na interface porem não marcados no nível inferior pertencentes a plantinterface21 em YinYLxXm21
                foreach (var splNi21 in statesplantinterface21)
                {
                    for (int c = 0; c < sp21; c++)
                    {
                        for (int i = 0; i < im21; i++)
                        {
                            if (splNi21.ToString() == String.Format("{0}|{1}", c, mstatesinterface21[i]))
                            {
                                YinYLxXm21.Add(splNi21);
                            }
                        }
                    }
                }
                //Console.WriteLine("--------------------------------------------------------------------------");
                //foreach (var splm in mstatesplantinterface21)
                //{

                //    Console.WriteLine("marketstates:{0}", splm);
                //}
                //Console.WriteLine("--------------------------------------------------------------------------");

                // loop for para definir verificar todos os estados adicionados a YinYLxXm21
                for (int z = 0; z < YinYLxXm21.Count(); z++)
                {
                    y21 = YinYLxXm21[z];
                    //Console.WriteLine("/tYinYLxXm21:{0}", y21);
                    //adciona em Yck_mk21 os estados de YinYLxXm21 e elimina todos aqueles que são marcados na plantinterface21
                    Yck_mk21.Add(y21);
                    foreach (var splm in mstatesplantinterface21)
                    {
                        if (Yck_mk21.Contains(splm) == true)
                        {
                            Yck_mk21.Remove(splm);
                            //Console.WriteLine("/Yck_mk21:{0}", y21);
                        }
                    }
                    //Console.WriteLine("--------------------------------------------------------------------------");

                    //loop for para verificar cada evento de pergunta
                    for (int i = 0; i < requestevents21.Count(); i++)
                    {
                        p21 = requestevents21[i];
                        // Cada estado alcançado em plantinterface21 por meio desse evento de pergunta é adicionado em alcanstatesfnd21 e alcanstatespend21
                        foreach (var t in plantinterface21.Transitions)
                        {
                            if (p21 == t.Trigger && y21 == t.Origin)
                            {
                                //Console.WriteLine("/Yck_mk21:{0}", y21);
                                alcanstatesfnd21.Add(t.Destination);
                                alcanstatespend21.Add(t.Destination);

                                /////// Answer(x), ou seja, adiciona as respostas possiveis a partir do evento de pergunta em questão 
                                foreach (var u1 in interface21.Transitions)
                                {
                                    if (u1.Trigger == p21)
                                    {
                                        alcanstatesinterface21.Add(u1.Destination);
                                    }
                                }
                                for (int k = 0; k < alcanstatesinterface21.Count(); k++)
                                {
                                    foreach (var u2 in interface21.Transitions)
                                    {
                                        if (u2.Origin == alcanstatesinterface21[k])
                                        {
                                            answerfunc21.Add(u2.Trigger);
                                            Sigmanfnd21 = answerfunc21;
                                            alcanstatesinterface22.Add(t.Destination);
                                        }
                                    }
                                }

                                // verifica se a partir dos estados alcanstatespend21 e Sigmanfnd21 alcançam eventos de resposta 
                                while (alcanstatespend21.Count() != 0 && Sigmanfnd21.Count() != 0)
                                {
                                    //pega o ultimo elemento de alcanstatespend21 para análisar e o remove
                                    y121 = alcanstatespend21[alcanstatespend21.Count() - 1];
                                    alcanstatespend21.Remove(y121);
                                    if (alcanstatesinterface22.Contains(y121))
                                    {
                                        for (int o21 = 0; o21 < alcanstatesinterface22.Count(); o21++)
                                        {
                                            if (alcanstatesinterface22[o21] == y121)
                                            {
                                                alcanstatesinterface23.Add(y121);
                                            }
                                        }
                                    }
                                    //verifica se cada evento da plantinterface21 esta contido em Sigmanfnd21 e o removo caso estiver e incrementa, 
                                    //com o estado alcançado por sigma21,  alcanstatesfnd21 e alcanstatespend21 caso o sigma21 seja um evento exclusivo 
                                    //da planta e alcanstatesfnd21 não contenha esse estado  
                                    for (int o = 0; o < eventsplantinterface21.Count(); o++)
                                    {
                                        sigma21 = eventsplantinterface21[o];
                                        foreach (var s21 in plantinterface21.Transitions)
                                        {
                                            if (s21.Origin == y121)
                                            {
                                                if (s21.Trigger == sigma21)
                                                {
                                                    y221 = s21.Destination;
                                                    if (Sigmanfnd21.Contains(sigma21) == true)
                                                    {
                                                        Sigmanfnd21.Remove(sigma21);
                                                        alcanstatesinterface23.Remove(alcanstatesinterface23[alcanstatesinterface23.Count() - 1]);
                                                    }
                                                    if (eventsplant21.Contains(sigma21) == true && alcanstatesfnd21.Contains(y221) == false)
                                                    {
                                                        alcanstatesfnd21.Add(y221);
                                                        alcanstatespend21.Add(y221);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //verifica se o sistema alcançou todas as respostas se não ele não passou
                                if (Sigmanfnd21.Count() != 0)
                                {
                                    //pega os estados que falham na planta||interface e tranforma só pra "planta" 
                                    foreach (var statesplantintver in alcanstatesinterface23)
                                    {
                                        for (int c21 = 0; c21 < sp21; c21++)
                                        {
                                            for (int i21 = 0; i21 < im21; i21++)
                                            {
                                                if (statesplantintver.ToString() == String.Format("{0}|{1}", c21, mstatesinterface21[i21]))
                                                {
                                                    failstatesplant.Add(c21);
                                                }
                                            }
                                        }
                                    }
                                    foreach (var statesplantver in statesplant21)
                                    {
                                        for (int c21 = 0; c21 < failstatesplant.Count(); c21++)
                                        {
                                            if (statesplantver.ToString() == String.Format("{0}", failstatesplant[c21]))
                                            {
                                                if (failstates.Contains(statesplantver) == false)
                                                {
                                                    failstates.Add(statesplantver);
                                                }
                                            }
                                        }
                                    }

                                    //Console.WriteLine("\t");
                                    //Console.WriteLine("\tNot Pass");
                                    g21 = 1;
                                }
                                alcanstatesfnd21.Clear();
                                alcanstatespend21.Clear();
                                alcanstatesinterface22.Clear();
                                alcanstatesinterface23.Clear();
                                alcanstatesinterface21.Clear();
                                failstatesplant.Clear();
                                answerfunc21.Clear();
                            }
                        }


                    }
                }

                //if (g21 == 0)
                //{
                //    Console.WriteLine("\t");
                //    Console.WriteLine("\tPass");
                //}
                // Console.WriteLine("--------------------------------------------------------------------------");

                //Console.WriteLine("Point 6:");
                //Console.WriteLine("/tYck_mk count:{0}", Yck_mk21.Count);
                //verifica se existem estados não marcados na planta e marcados na interface
                if (Yck_mk21.Count == 0)
                {
                    //Console.WriteLine("\t");
                    //Console.WriteLine("\tPass");
                }
                else
                {
                    //for (int o2 = 0; o2 < Yck_mk21.Count(); o2++)
                    //{
                    //    Console.WriteLine("/tYck_mk21:{0}", Yck_mk21[o2]);
                    //} 
                    //declaração de variaveis
                    failstatesp6.AddRange(Yck_mk21);
                    nalcanstatespend21 = plantinterface21.MarkedStates.ToList();
                    nalcanstatesfnd21 = plantinterface21.MarkedStates.ToList();
                    int n21 = 0;
                    int b21 = 0;
                    //verificação ciclica ponto 6
                    while (nalcanstatespend21.Count() != 0)
                    {
                        n21 = n21 + 1;
                        //retira o ultimo elemento da lista em questão
                        y321 = nalcanstatespend21[nalcanstatespend21.Count() - 1];
                        //adiciona em Y2221 apenas estados nunca visitados
                        if (Y2221.Contains(y321) == false)
                        {
                            Y2221.Add(y321);
                        }
                        //Console.WriteLine("\ttest0:{0}", y321);
                        //Console.WriteLine("\ttestcount1:{0}", nalcanstatespend21.Count());
                        nalcanstatespend21.Remove(y321);
                        //Console.WriteLine("\ttestcount2:{0}", nalcanstatespend21.Count());   
                        //seguindo apenas eventos exclusivos da planta
                        for (int i21 = 0; i21 < eventsplant21.Count(); i21++)
                        {
                            sigma121 = eventsplant21[i21];
                            //anda inversamente pela plantainterface apartir de estados marcados por meio de eventos exclusivos a planta
                            foreach (var s5 in plantinterface21.Transitions)
                            {
                                if (s5.Destination == y321)
                                {
                                    if (s5.Trigger == sigma121)
                                    {
                                        //adiciona um novo estado a y21 e y2221 caso y2221 não contenha o novo estado
                                        if (Y2221.Contains(s5.Origin) == false)
                                        {
                                            Y21.Add(s5.Origin);
                                            Y2221.Add(s5.Origin);
                                        }
                                    }
                                }
                            }
                            //caso y21 seja diferente de zero ou seja o sistema alcançou novos estados
                            if (Y21.Count() != 0)
                            {
                                //analisa cada estado alcançado
                                for (int i3 = 0; i3 < Y21.Count(); i3++)
                                {

                                    y421 = Y21[i3];
                                    //Y2221.Add(y421);

                                    //Console.WriteLine("\ttest2:{0}", y421);
                                    //Console.WriteLine("\ttest3:{0}", Y2221[Y2221.Count() - 1]);
                                    //se o estado ainda não foi visitado
                                    if (nalcanstatesfnd21.Contains(y421) == false)
                                    {
                                        // Console.WriteLine("\ttest3:{0}", y421);
                                        //adiciona em nalcanstatesfnd21 e nalcanstatespend21
                                        nalcanstatesfnd21.Add(y421);
                                        nalcanstatespend21.Add(y421);
                                        if (Yck_mk21.Contains(y421) == true)
                                        {
                                            Yck_mk21.Remove(y421);
                                            failstatesp6.Remove(y421);
                                            //Console.WriteLine("\ttest4:{0}", Yck_mk21.Count());
                                        }
                                        if (Yck_mk21.Count() == 0 && b21 == 0)
                                        {
                                            //Console.WriteLine("\t");
                                            //Console.WriteLine("\tPass");
                                            b21 = b21 + 1;
                                        }
                                    }
                                }
                            }
                            Y21.Clear();
                        }
                        // Console.WriteLine("\ttest2");
                        if (b21 != 0)
                        {
                            break;
                        }
                    }
                    if (nalcanstatespend21.Count() == 0 && b21 == 0)
                    {
                        //Console.WriteLine("\t");
                        //Console.WriteLine("\tNot Pass");
                        failstates.AddRange(failstatesp6);
                    }
                }

                //Console.WriteLine("--------------------------------------------------------------------------");

                List<AbstractEvent> EventosTransi = new List<AbstractEvent>(); //Os eventos que partem do estado em questão    
                List<AbstractState> EstadosAlcan = new List<AbstractState>();//Os estados alcançaveis a partir do estado em questão 
                List<AbstractState> OrdemEstadosAlcan = new List<AbstractState>();//Ordem correta dos estados alcançaveis
                List<AbstractState> OrdemEstadosAlcan2 = new List<AbstractState>();//Ordem da primeira aparição estados desde o começo
                List<AbstractState> OrdemEstadosAlcan3 = new List<AbstractState>();//Ordem correta dos estados alcançaveis desde o começo
                List<AbstractState> OrdemEstadosAlcanFim = new List<AbstractState>();//Ordem dos novos estados em cada iteração
                List<AbstractState> OrdemEstadosAlcanFim2 = new List<AbstractState>();//Ordem dos novos estados desde o começo
                List<int> FimEstados = new List<int>();

                var transG = new List<Transition>();
                var s = new List<State>();

                //lista com os nomes dos estados
                for (int u = 1; u < k21.States.Count(); u++)
                {
                    FimEstados.Add(u);
                }

                //Cria o estado inicial verificando sua marcação
                if (k21.MarkedStates.Contains(k21.InitialState))
                {
                    s.Add(new State(0.ToString(), Marking.Marked));
                }
                else
                {
                    s.Add(new State(0.ToString(), Marking.Unmarked));
                }

                OrdemEstadosAlcan2.Add(k21.InitialState);//adiciona o primeiro estado criado

                //iteração para criação dos novos estadose transições
                for (int c = 0; c < k21.States.Count(); c++)
                {
                    //verifica os eventos e estados alcançaveis pelo estado tratado
                    foreach (var t in k21.Transitions)
                    {
                        if (c == 0)
                        {
                            if (t.Origin == k21.InitialState)
                            {
                                EstadosAlcan.Add(t.Destination);
                                EventosTransi.Add(t.Trigger);
                            }
                        }
                        else
                        {
                            if (t.Origin == OrdemEstadosAlcan2[c])
                            {
                                EstadosAlcan.Add(t.Destination);
                                EventosTransi.Add(t.Trigger);
                            }
                        }
                    }

                    var OrdemEventosTransi = EventosTransi.OrderBy(n => n.ToString()).ToArray();//ordena os eventos 

                    //ordena os estados a partir dos eventos ordenandos e já adiciona em OrdemEstadosAlcan3
                    for (int n = 0; n < OrdemEventosTransi.Count(); n++)
                    {
                        for (int i = 0; i < EventosTransi.Count(); i++)
                        {
                            if (EventosTransi[i] == OrdemEventosTransi[n])
                            {
                                OrdemEstadosAlcan.Add(EstadosAlcan[i]);
                                OrdemEstadosAlcan3.Add(EstadosAlcan[i]);
                            }
                        }
                    }

                    //cria os novos estados e adiciona em OrdemEstadosAlcan2, OrdemEstadosAlcanFim e OrdemEstadosAlcanFim2
                    for (int h = 0; h < OrdemEstadosAlcan.Count(); h++)
                    {
                        //se for o estado inicial
                        if (OrdemEstadosAlcan[h] == k21.InitialState)
                        {
                            OrdemEstadosAlcanFim.Add(s[0]);
                            OrdemEstadosAlcanFim2.Add(s[0]);
                        }
                        else
                        {
                            //se OrdemEstadosAlcan2 não conter OrdemEstadosAlcan[h] quer dizer que pode ser criado um novo estado, 
                            //caso contrairio não é necessário, porém deve ser adicionado em uma nova tabela
                            if (OrdemEstadosAlcan2.Contains(OrdemEstadosAlcan[h]) == false)
                            {
                                if (k21.MarkedStates.Contains(OrdemEstadosAlcan[h]))
                                {
                                    s.Add(new State(FimEstados[0].ToString(), Marking.Marked));
                                    OrdemEstadosAlcanFim.Add(s[FimEstados[0]]);
                                    OrdemEstadosAlcanFim2.Add(s[FimEstados[0]]);
                                    FimEstados.Remove(FimEstados[0]);

                                }
                                else
                                {
                                    s.Add(new State(FimEstados[0].ToString(), Marking.Unmarked));
                                    OrdemEstadosAlcanFim.Add(s[FimEstados[0]]);
                                    OrdemEstadosAlcanFim2.Add(s[FimEstados[0]]);
                                    FimEstados.Remove(FimEstados[0]);
                                }
                                OrdemEstadosAlcan2.Add(OrdemEstadosAlcan[h]);
                            }
                            else
                            {
                                for (int j = 0; j < OrdemEstadosAlcan3.Count(); j++)
                                {

                                    if (OrdemEstadosAlcan[h] == OrdemEstadosAlcan3[j])
                                    {
                                        OrdemEstadosAlcanFim.Add(OrdemEstadosAlcanFim2[j]);
                                        OrdemEstadosAlcanFim2.Add(OrdemEstadosAlcanFim2[j]);
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    //cria as transições de acordo com as listas OrdemEventosTransi e OrdemEstadosAlcanFim
                    for (int y = 0; y < OrdemEventosTransi.Count(); y++)
                    {
                        if (c == 0)
                        {
                            if (failstates.Contains(s[0]) == false || failstates.Contains(OrdemEstadosAlcanFim[y]) == false)
                            {
                                transG.Add(new Transition(s[0], OrdemEventosTransi[y], OrdemEstadosAlcanFim[y]));
                            }
                        }
                        else
                        {
                            if (failstates.Contains(s[c]) == false || failstates.Contains(OrdemEstadosAlcanFim[y]) == false)
                            {
                                transG.Add(new Transition(s[c], OrdemEventosTransi[y], OrdemEstadosAlcanFim[y]));
                            }
                        }
                    }

                    OrdemEstadosAlcan.Clear();
                    EventosTransi.Clear();
                    OrdemEstadosAlcanFim.Clear();
                    EstadosAlcan.Clear();
                }

                //cria o automato novo
                k21 = new DeterministicFiniteAutomaton(transG, s[0], k21.Name.ToString());

                k21 = k21.Trim;
                newsimplifyName(k21, out k21);
                //Console.WriteLine("K1 fim: {0}", k21.States.Count());


                OrdemEstadosAlcanFim2.Clear();
                OrdemEstadosAlcan2.Clear();
                OrdemEstadosAlcan3.Clear();
                failstates.Clear();
            }

            SFIM = k21;
        }

        private static void Verificationcontrolability(DeterministicFiniteAutomaton S, DeterministicFiniteAutomaton G, int limit)
        {
            var statesAndEventsList = S.DisabledEvents(G);
            int i = 0;
            int t = 0;
            //Console.WriteLine("UncontrollableEvents: {0}", G.UncontrollableEvents.Count());
            //var last = pairStateEventList.Value.Count();
            foreach (var pairStateEventList in statesAndEventsList)
            {

                var last = pairStateEventList.Value.Count();

                foreach (var _event in pairStateEventList.Value)
                {
                    if (G.UncontrollableEvents.Contains(_event))
                    {
                        //if(t == 0)
                        //{
                        //    Console.WriteLine("Disabled Events:");
                        //}
                        //Console.WriteLine("\tState: {0}", pairStateEventList.Key.ToString());
                        //Console.WriteLine("\t\tEvent: {0}", _event.ToString());
                        t++;
                    }

                }
                if (i == limit - 1)
                {
                    if (t != 0)
                    {
                        Console.Write("\tNot Pass");
                        Console.Write("\n");
                    }
                    if (t == 0)
                    {
                        Console.Write("\tPass");
                        Console.Write("\n");
                    }
                }

                if (++i >= limit) break;
            }
        }

        private static void ShowDisablement2(DeterministicFiniteAutomaton S, DeterministicFiniteAutomaton G, int limit)
        {
            var statesAndEventsList = S.DisabledEvents(G);
            
            int i = 0;
            foreach (var pairStateEventList in statesAndEventsList)
            {
                Console.WriteLine("\tState: {0}", pairStateEventList.Key.ToString());

                foreach (var _event in pairStateEventList.Value)
                {
                    Console.WriteLine("\t\tEvent: {0}", _event.ToString());
                }
                Console.Write("\n");

                if (++i >= limit) break;
            }
        }

        private static void newsimplifyName(DeterministicFiniteAutomaton G, out DeterministicFiniteAutomaton Gfim)
        {
            List<AbstractEvent> EventosTransi = new List<AbstractEvent>(); //Os eventos que partem do estado em questão    
            List<AbstractState> EstadosAlcan = new List<AbstractState>();//Os estados alcançaveis a partir do estado em questão 
            List<AbstractState> OrdemEstadosAlcan = new List<AbstractState>();//Ordem correta dos estados alcançaveis
            List<AbstractState> OrdemEstadosAlcan2 = new List<AbstractState>();//Ordem da primeira aparição estados desde o começo
            List<AbstractState> OrdemEstadosAlcan3 = new List<AbstractState>();//Ordem correta dos estados alcançaveis desde o começo
            List<AbstractState> OrdemEstadosAlcanFim = new List<AbstractState>();//Ordem dos novos estados em cada iteração
            List<AbstractState> OrdemEstadosAlcanFim2 = new List<AbstractState>();//Ordem dos novos estados desde o começo
            List<int> FimEstados = new List<int>();

            var transG = new List<Transition>();
            var s = new List<State>();

            //lista com os nomes dos estados
            for (int u = 1; u < G.States.Count(); u++)
            {
                FimEstados.Add(u);
            }

            //Cria o estado inicial verificando sua marcação
            if (G.MarkedStates.Contains(G.InitialState))
            {
                s.Add(new State(0.ToString(), Marking.Marked));
            }
            else
            {
                s.Add(new State(0.ToString(), Marking.Unmarked));
            }

            OrdemEstadosAlcan2.Add(G.InitialState);//adiciona o primeiro estado criado

            //iteração para criação dos novos estadose transições
            for (int c = 0; c < G.States.Count(); c++)
            {
                //verifica os eventos e estados alcançaveis pelo estado tratado
                foreach (var t in G.Transitions)
                {
                    if (c == 0)
                    {
                        if (t.Origin == G.InitialState)
                        {
                            EstadosAlcan.Add(t.Destination);
                            EventosTransi.Add(t.Trigger);
                        }
                    }
                    else
                    {
                        if (t.Origin == OrdemEstadosAlcan2[c])
                        {
                            EstadosAlcan.Add(t.Destination);
                            EventosTransi.Add(t.Trigger);
                        }
                    }
                }

                var OrdemEventosTransi = EventosTransi.OrderBy(n => n.ToString()).ToArray();//ordena os eventos 

                //ordena os estados a partir dos eventos ordenandos e já adiciona em OrdemEstadosAlcan3
                for (int n = 0; n < OrdemEventosTransi.Count(); n++)
                {
                    for (int i = 0; i < EventosTransi.Count(); i++)
                    {
                        if (EventosTransi[i] == OrdemEventosTransi[n])
                        {
                            OrdemEstadosAlcan.Add(EstadosAlcan[i]);
                            OrdemEstadosAlcan3.Add(EstadosAlcan[i]);
                        }
                    }
                }

                //cria os novos estados e adiciona em OrdemEstadosAlcan2, OrdemEstadosAlcanFim e OrdemEstadosAlcanFim2
                for (int h = 0; h < OrdemEstadosAlcan.Count(); h++)
                {
                    //se for o estado inicial
                    if (OrdemEstadosAlcan[h] == G.InitialState)
                    {
                        OrdemEstadosAlcanFim.Add(s[0]);
                        OrdemEstadosAlcanFim2.Add(s[0]);
                    }
                    else
                    {
                        //se OrdemEstadosAlcan2 não conter OrdemEstadosAlcan[h] quer dizer que pode ser criado um novo estado, 
                        //caso contrairio não é necessário, porém deve ser adicionado em uma nova tabela
                        if (OrdemEstadosAlcan2.Contains(OrdemEstadosAlcan[h]) == false)
                        {
                            if (G.MarkedStates.Contains(OrdemEstadosAlcan[h]))
                            {
                                s.Add(new State(FimEstados[0].ToString(), Marking.Marked));
                                OrdemEstadosAlcanFim.Add(s[FimEstados[0]]);
                                OrdemEstadosAlcanFim2.Add(s[FimEstados[0]]);
                                FimEstados.Remove(FimEstados[0]);

                            }
                            else
                            {
                                s.Add(new State(FimEstados[0].ToString(), Marking.Unmarked));
                                OrdemEstadosAlcanFim.Add(s[FimEstados[0]]);
                                OrdemEstadosAlcanFim2.Add(s[FimEstados[0]]);
                                FimEstados.Remove(FimEstados[0]);
                            }
                            OrdemEstadosAlcan2.Add(OrdemEstadosAlcan[h]);
                        }
                        else
                        {
                            for (int j = 0; j < OrdemEstadosAlcan3.Count(); j++)
                            {

                                if (OrdemEstadosAlcan[h] == OrdemEstadosAlcan3[j])
                                {
                                    OrdemEstadosAlcanFim.Add(OrdemEstadosAlcanFim2[j]);
                                    OrdemEstadosAlcanFim2.Add(OrdemEstadosAlcanFim2[j]);
                                    break;
                                }
                            }
                        }
                    }
                }

                //cria as transições de acordo com as listas OrdemEventosTransi e OrdemEstadosAlcanFim
                for (int y = 0; y < OrdemEventosTransi.Count(); y++)
                {
                    if (c == 0)
                    {
                        transG.Add(new Transition(s[0], OrdemEventosTransi[y], OrdemEstadosAlcanFim[y]));
                    }
                    else
                    {
                        transG.Add(new Transition(s[c], OrdemEventosTransi[y], OrdemEstadosAlcanFim[y]));
                    }
                }

                OrdemEstadosAlcan.Clear();
                EventosTransi.Clear();
                OrdemEstadosAlcanFim.Clear();
                EstadosAlcan.Clear();
            }

            //cria o automato novo
            Gfim = new DeterministicFiniteAutomaton(transG, s[0], G.Name.ToString());
            OrdemEstadosAlcanFim2.Clear();
            OrdemEstadosAlcan2.Clear();
            OrdemEstadosAlcan3.Clear();


        }

        private static void isomorf(DeterministicFiniteAutomaton G1, DeterministicFiniteAutomaton G2)
        {
            List<string> tran1 = new List<string>();
            List<string> tran2 = new List<string>();
            List<string> estadosmarcados1 = new List<string>();//Ordem correta dos estados alcançaveis
            List<string> estadosmarcados2 = new List<string>();//Ordem da primeira aparição estados desde o começo
            DeterministicFiniteAutomaton G1fim;
            DeterministicFiniteAutomaton G2fim;

            newsimplifyName(G1, out G1fim);

            newsimplifyName(G2, out G2fim);
            int x = 0;

            foreach (var s1 in G1fim.MarkedStates)
            {
                estadosmarcados1.Add(s1.ToString());
            }


            foreach (var s2 in G2fim.MarkedStates)
            {
                estadosmarcados2.Add(s2.ToString());
            }


            var Ordemmarketstate1 = estadosmarcados1.OrderBy(n => n.ToString()).ToArray();
            var Ordemmarketstate2 = estadosmarcados2.OrderBy(n => n.ToString()).ToArray();

            foreach (var t1 in G1fim.Transitions)
            {
                tran1.Add(t1.ToString());
            }

            foreach (var t2 in G2fim.Transitions)
            {
                tran2.Add(t2.ToString());
            }

            var OrdemTran1 = tran1.OrderBy(n => n.ToString()).ToArray();
            var OrdemTran2 = tran2.OrderBy(n => n.ToString()).ToArray();

            if (G1fim.InitialState.ToString() != G2fim.InitialState.ToString())
            {
                x = 1;
            }

            if (estadosmarcados1.Count() == estadosmarcados2.Count())
            {
                for (int c1 = 0; c1 < estadosmarcados2.Count(); c1++)
                {
                    if (Ordemmarketstate1[c1] != Ordemmarketstate2[c1])
                    {
                        x = 1;
                    }
                }
            }
            else
            {
                x = 1;
            }


            if (tran1.Count() == tran2.Count())
            {
                for (int c = 0; c < tran1.Count(); c++)
                {
                    if (OrdemTran1[c] != OrdemTran2[c])
                    {
                        x = 1;
                    }
                }
            }
            else
            {
                x = 1;
            }

            if (x == 0)
            {
                Console.WriteLine("\tThe automatons are isomorfs");
            }
            else
            {
                Console.WriteLine("\tThe automatons aren't isomorfs");
            }
        }


        private static void Main()
        {

            List<DeterministicFiniteAutomaton> plants, plants1, plants21, plants22, plants23, specs, especs1m, especs2m, specs1, specs21, specs22, specs23, specs2m, specsmono, interfaces1, sup1, sup2, plantaas, plantaas1, plantaas2;


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("------------------------HIERARQUICAL PROGRAM------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("------------------------------TOYEXEMPLO-----------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------");

            toyexemple(out plants, out specs, out interfaces1);
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("\tTotal Plants: {0}", plants.Count());
            Console.WriteLine("\tTotal Specifications: {0}", specs.Count());
            Console.WriteLine("\tTotal Interface Serial Case: {0}", interfaces1.Count());

            var timer = new Stopwatch();
            string x;
            int h;
            x = "";
            while (x != "0")
            {
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("Choose one case:");
                Console.WriteLine("\tMonolithic Case = 1");
                Console.WriteLine("\tParallel Case = 2");
                Console.WriteLine("\tExit = 0");
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("Your Choice:");
                x = Console.ReadLine();
                while (x != "1" && x != "2"  && x != "0")
                {
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Error!! Enter another input!!");
                    Console.WriteLine("Your Choice:");
                    x = Console.ReadLine();
                }
                h = 0;
                if (x == "1")
                { h = 1; }
                else if (x == "2")
                { h = 2; }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (h == 1)
                {
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Case ----------------------------------------------------------");
                    Console.WriteLine("--------------------------------------------------------------------------");

                    var Specification = DeterministicFiniteAutomaton.ParallelComposition(specs);
                    var Plant = DeterministicFiniteAutomaton.ParallelComposition(plants);
                    var K = Plant.ParallelCompositionWith(Specification);
                    Console.WriteLine("Plant");
                    Console.WriteLine("\tstates: {0}", Plant.Size);
                    Console.WriteLine("\tTransitions: {0}", Plant.Transitions.Count());
                    Console.WriteLine("Specification");
                    Console.WriteLine("\tStates: {0}", Specification.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification.Transitions.Count());
                    Console.WriteLine("K:");
                    Console.WriteLine("\tStates: {0}", K.Size);
                    Console.WriteLine("\tTransitions: {0}", K.Transitions.Count());
                    Console.WriteLine("--------------------------------------------------------------------------");
                    //// Controllability
                    if (K.IsControllable(Plant))
                    {
                        Console.WriteLine("\tK is controllable");
                    }
                    else
                    {
                        Console.WriteLine("\tK is not controllable");
                    }
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Supervisor :");
                    //timer.Restart();
                    var supmono = DeterministicFiniteAutomaton.MonolithicSupervisor(plants, specs, true);
                    //timer.Stop();
                    Console.WriteLine("\tStates: {0}", supmono.Size);
                    Console.WriteLine("\tTransitions: {0}", supmono.Transitions.Count());
                    Console.WriteLine("\tmarketstarte: {0}", supmono.MarkedStates.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);
                }              
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (h == 2)
                {
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("----------------------------Parallel Case---------------------------------");
                    Console.WriteLine("--------------------------------------------------------------------------");
                    //timer.Restart();

                    //H11
                    Console.WriteLine("High Level (H11)");
                    plants1 = new[] { plants[0]}.ToList(); 
                    Console.WriteLine("\tTotal Plants : {0}", plants1.Count());
                    specs1 = new[] { specs[0] }.ToList();
                    Console.WriteLine("\tTotal Specifications : {0}", specs1.Count());

                    //H21
                    Console.WriteLine("Low Level (H21)");
                    plants21 = new[] { plants[1], plants[2], plants[3]}.ToList();
                    Console.WriteLine("\tTotal Plants : {0}", plants21.Count());
                    specs21 = new[] { specs[1]}.ToList();
                    Console.WriteLine("\tTotal Specifications: {0}", specs21.Count());

                    //H22
                    Console.WriteLine("Low Level (H22)");
                    plants22 = new[] { plants[4], plants[5], plants[6] }.ToList();
                    Console.WriteLine("\tTotal Plants : {0}", plants22.Count());
                    specs22 = new[] { specs[2]}.ToList();
                    Console.WriteLine("\tTotal Specifications: {0}", specs22.Count());

                    //H23
                    Console.WriteLine("Low Level (H23)");
                    plants23 = new[] { plants[7], plants[8], plants[9] }.ToList();
                    Console.WriteLine("\tTotal Plants : {0}", plants23.Count());
                    specs23 = new[] { specs[3]}.ToList();
                    Console.WriteLine("\tTotal Specifications: {0}", specs23.Count());

                    //Interface (I21, I22 and I23)
                    Console.WriteLine("Interfaces");
                    Console.WriteLine("\tTotal: {0}", interfaces1.Count());            
                    Console.WriteLine("--------------------------------------------------------------------------");

                    Console.WriteLine("High Level Composition (H11):");
                    var Specification1 = DeterministicFiniteAutomaton.ParallelComposition(specs1);
                    var Plant1 = DeterministicFiniteAutomaton.ParallelComposition(plants1);
                    var Plantint1 = Plant1.ParallelCompositionWith(interfaces1);
                    var interface1 = DeterministicFiniteAutomaton.ParallelComposition(interfaces1);
                    var K1 = Plant1.ParallelCompositionWith(interface1, Specification1);
                    Console.WriteLine("Plant");
                    Console.WriteLine("\tStates: {0}", Plant1.Size);
                    Console.WriteLine("\tTransitions: {0}", Plant1.Transitions.Count());
                    Console.WriteLine("Specification");
                    Console.WriteLine("\tStates: {0}", Specification1.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification1.Transitions.Count());
                    Console.WriteLine("Interfaces");
                    Console.WriteLine("\tStates: {0}", interface1.Size);
                    Console.WriteLine("\tTransitions: {0}", interface1.Transitions.Count());
                    Console.WriteLine("K:");
                    Console.WriteLine("\tStates: {0}", K1.Size);
                    Console.WriteLine("\tTransitions: {0}", K1.Transitions.Count());
                    if (K1.IsControllable(Plantint1))
                    {
                        Console.WriteLine("\tK is controllable");
                    }
                    else
                    {
                        Console.WriteLine("\tK is not controllable");
                    }

                    //ShowDisablement2(K1, Plant1, 60);//condat
                    //Console.WriteLine("Ktrim:");
                    //Console.WriteLine("\tStates: {0}", K1.Trim.Size);
                    //Console.WriteLine("\tTransitions: {0}", K1.Trim.Transitions.Count());

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Low Level Composition (H21):");
                    var Specification21 = DeterministicFiniteAutomaton.ParallelComposition(specs21);
                    var Plant21 = DeterministicFiniteAutomaton.ParallelComposition(plants21);
                    var interface21 = interfaces1[0];
                    var K21 = Plant21.ParallelCompositionWith(interface21, Specification21);
                    Console.WriteLine("Plant");
                    Console.WriteLine("\tStates: {0}", Plant21.Size);
                    Console.WriteLine("\tTransitions: {0}", Plant21.Transitions.Count());
                    Console.WriteLine("Specification");
                    Console.WriteLine("\tStates: {0}", Specification21.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification21.Transitions.Count());
                    Console.WriteLine("Interfaces");
                    Console.WriteLine("\tStates: {0}", interface21.Size);
                    Console.WriteLine("\tTransitions: {0}", interface21.Transitions.Count());
                    Console.WriteLine("K:");
                    Console.WriteLine("\tStates: {0}", K21.Size);
                    Console.WriteLine("\tTransitions: {0}", K21.Transitions.Count());
                    if (K21.IsControllable(Plant21))
                    {
                        Console.WriteLine("\tK is controllable");
                    }
                    else
                    {
                        Console.WriteLine("\tK is not controllable");
                    }
                    //Console.WriteLine("Ktrim:");
                    //Console.WriteLine("\tStates: {0}", K2.Trim.Size);
                    //Console.WriteLine("\tTransitions: {0}", K2.Trim.Transitions.Count());

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Low Level Composition (H22):");
                    var Specification22 = DeterministicFiniteAutomaton.ParallelComposition(specs22);
                    var Plant22 = DeterministicFiniteAutomaton.ParallelComposition(plants22);
                    var interface22 = interfaces1[1];
                    var K22 = Plant22.ParallelCompositionWith(interface22, Specification22);
                    Console.WriteLine("Plant");
                    Console.WriteLine("\tStates: {0}", Plant22.Size);
                    Console.WriteLine("\tTransitions: {0}", Plant22.Transitions.Count());
                    Console.WriteLine("Specification");
                    Console.WriteLine("\tStates: {0}", Specification22.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification22.Transitions.Count());                    
                    Console.WriteLine("Interfaces");
                    Console.WriteLine("\tStates: {0}", interface22.Size);
                    Console.WriteLine("\tTransitions: {0}", interface22.Transitions.Count());
                    Console.WriteLine("K:");
                    Console.WriteLine("\tStates: {0}", K22.Size);
                    Console.WriteLine("\tTransitions: {0}", K22.Transitions.Count());
                    if (K22.IsControllable(Plant22))
                    {
                        Console.WriteLine("\tK is controllable");
                    }
                    else
                    {
                        Console.WriteLine("\tK is not controllable");
                    }
                    //Console.WriteLine("Ktrim:");
                    //Console.WriteLine("\tStates: {0}", K2.Trim.Size);
                    //Console.WriteLine("\tTransitions: {0}", K2.Trim.Transitions.Count());

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Low Level Composition (H23):");
                    var Specification23 = DeterministicFiniteAutomaton.ParallelComposition(specs23);
                    var Plant23 = DeterministicFiniteAutomaton.ParallelComposition(plants23);
                    var interface23 = interfaces1[2];
                    var K23 = Plant23.ParallelCompositionWith(interface23, Specification23);
                    Console.WriteLine("Plant");
                    Console.WriteLine("\tStates: {0}", Plant23.Size);
                    Console.WriteLine("\tTransitions: {0}", Plant23.Transitions.Count());
                    Console.WriteLine("Specification");
                    Console.WriteLine("\tStates: {0}", Specification23.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification23.Transitions.Count());
                    Console.WriteLine("Interfaces");
                    Console.WriteLine("\tStates: {0}", interface23.Size);
                    Console.WriteLine("\tTransitions: {0}", interface23.Transitions.Count());
                    Console.WriteLine("K:");
                    Console.WriteLine("\tStates: {0}", K23.Size);
                    Console.WriteLine("\tTransitions: {0}", K23.Transitions.Count());
                    if (K23.IsControllable(Plant23))
                    {
                        Console.WriteLine("\tK is controllable");
                    }
                    else
                    {
                        Console.WriteLine("\tK is not controllable");
                    }
                    //Console.WriteLine("Ktrim:");
                    //Console.WriteLine("\tStates: {0}", K2.Trim.Size);
                    //Console.WriteLine("\tTransitions: {0}", K2.Trim.Transitions.Count());
                    Console.WriteLine("--------------------------------------------------------------------------");

                    //timer.Stop();
                    //Console.WriteLine("Computation Time for the compositions: {0}", timer.ElapsedMilliseconds / 1000.0);
                    //Console.WriteLine("--------------------------------------------------------------------------");

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ////Sigma_A and Sigma_R////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    var eventsinterface21 = interface21.Events.ToList();//SigmaG_I21

                    //Find Events in Interface I21
                    //for (int c = 0; c < eventsinterface21.Count(); c++)
                    //{
                    //    Console.WriteLine("\teventos interface 21: {0}", eventsinterface21[c]);
                    //}
                    ////Console.WriteLine("\teventos interface: {0}", eventsinterface21);

                    var eventsinterface22 = interface22.Events.ToList();//SigmaG_I22  

                    //Find Events in Interface I22
                    //for (int c = 0; c < eventsinterface22.Count(); c++)
                    //{
                    //    Console.WriteLine("\teventos interface 22: {0}", eventsinterface22[c]);
                    //}
                    ////Console.WriteLine("\teventos interface: {0}", eventsinterface22);

                    var eventsinterface23 = interface23.Events.ToList();//SigmaG_I23 

                    //Find Events in Interface I23
                    //for (int c = 0; c < eventsinterface23.Count(); c++)
                    //{
                    //    Console.WriteLine("\teventos interface 23: {0}", eventsinterface23[c]);
                    //}
                    ////Console.WriteLine("\teventos interface: {0}", eventsinterface23);

                    var requestevents21 = new[] { eventsinterface21[0] }.ToList();//SigmaR
                    var answerevents21 = new[] { eventsinterface21[1] }.ToList();//SigmaA

                    var requestevents22 = new[] { eventsinterface22[1] }.ToList();//SigmaR
                    var answerevents22 = new[] { eventsinterface22[0], eventsinterface22[2] }.ToList();//SigmaA

                    var requestevents23 = new[] { eventsinterface23[0] }.ToList();//SigmaR
                    var answerevents23 = new[] { eventsinterface23[1] }.ToList();//SigmaA


                    ////Supervisors Compositions///////////////////////////////////////////////////////////////////////
                    //High Level Composition/////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Supervisor High Level (H11):");
                    plants1.Add(interface1);
                    //timer.Restart();
                    var supH11 = DeterministicFiniteAutomaton.MonolithicSupervisor(plants1, specs1, true);
                    //timer.Stop();
                    Console.WriteLine("\tStates: {0}", supH11.Size);
                    Console.WriteLine("\tTransitions: {0}", supH11.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);
                    //supH11.drawSVGFigure("supH11", true);

                    //Low Level Composition/////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Supervisor Low Level (H21):");

                    specs21.Add(interface21);
                    timer.Restart();
                    var supH21 = DeterministicFiniteAutomaton.MonolithicSupervisor(plants21, specs21, true);
                    //timer.Stop();
                    Console.WriteLine("\tStates: {0}", supH21.Size);
                    Console.WriteLine("\tTransitions: {0}", supH21.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Supervisor Low Level (H22):");
                    specs22.Add(interface22);
                    //timer.Restart();
                    var supH22 = DeterministicFiniteAutomaton.MonolithicSupervisor(plants22, specs22, true);
                    //timer.Stop();
                    Console.WriteLine("\tStates: {0}", supH22.Size);
                    Console.WriteLine("\tTransitions: {0}", supH22.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Supervisor Low Level (H23):");
                    specs23.Add(interface23);
                    //timer.Restart();
                    var supH23 = DeterministicFiniteAutomaton.MonolithicSupervisor(plants23, specs23, true);
                    //timer.Stop();
                    Console.WriteLine("\tStates: {0}", supH23.Size);
                    Console.WriteLine("\tTransitions: {0}", supH23.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    //Flat Supervisor////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("\tSupervisors Composition:");
                    var s11 = supH11.ParallelCompositionWith(supH22, supH23, supH21, interface21, interface22, interface23);
                    Console.WriteLine("\tStates: {0}", s11.Size);
                    Console.WriteLine("\tTransitions: {0}", s11.Trim.Transitions.Count());

                    //Monolithic Supervisor//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("\tMonolithic Supervisor:");
                    var smono = DeterministicFiniteAutomaton.MonolithicSupervisor(plants, specs, true);
                    Console.WriteLine("\tStates: {0}", smono.Size);
                    Console.WriteLine("\tTransitions: {0}", smono.Transitions.Count());
                    Console.WriteLine("--------------------------------------------------------------------------");

                    //Isomorfism//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("isomorfism test");
                    isomorf(s11, smono);

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Verification of properties:");
                    Console.WriteLine("--------------------------------------------------------------------------");

                    Console.WriteLine("Relationship between H11 and H21");
                    var supplan21 = supH21.ParallelCompositionWith(Plant21);
                    var supplan11 = supH11.ParallelCompositionWith(Plant1);
                    Verificationpoint(supplan11, supplan21, interface21, requestevents21, answerevents21);

                    Console.WriteLine("--------------------------------------------------------------------------");

                    Console.WriteLine("Relationship between H11 and H22");
                    var supplan22 = supH22.ParallelCompositionWith(Plant22);
                    Verificationpoint(supplan11, supplan22, interface22, requestevents22, answerevents22);

                    Console.WriteLine("--------------------------------------------------------------------------");

                    Console.WriteLine("Relationship between H11 and H23");
                    var supplan23 = supH23.ParallelCompositionWith(Plant23);
                    Verificationpoint(supplan11, supplan23, interface23, requestevents23, answerevents23);

                    //////Synthesis////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //Console.WriteLine("--------------------------------------------------------------------------");
                    //Console.WriteLine("Synthesis Program:");
                    //Console.WriteLine("--------------------------------------------------------------------------");

                    //var answereventslist = new[] { eventsinterface21[1], eventsinterface22[0], eventsinterface22[2], eventsinterface23[1] }.ToList();// list with all response events from all interfaces connected inferiorly in sequence

                    //var numberanswer = new[] { 1, 2, 1 }.ToList();//Number of response events from each interface connected inferiorly

                    //Console.WriteLine("--------------------------------------------------------------------------");

                    //Console.WriteLine("Synthesis H11");
                    //sintesesupHHISC(Plant1, Specification1, interfaces1, answereventslist, numberanswer, out var supH11);
                    //Console.WriteLine("\tStates: {0}", supH11.Size);
                    //Console.WriteLine("\tTransitions: {0}", supH11.Transitions.Count());

                    //Console.WriteLine("--------------------------------------------------------------------------");

                    //Console.WriteLine("Synthesis H21");
                    //sintesesupLHISC(Plant21, Specification21, interface21, requestevents21, answerevents21, out var  supH21);
                    //Console.WriteLine("\tStates: {0}", supH21.Size);
                    //Console.WriteLine("\tTransitions: {0}", supH21.Transitions.Count());

                    //Console.WriteLine("--------------------------------------------------------------------------");

                    //Console.WriteLine("Synthesis H22");
                    //sintesesupLHISC(Plant22, Specification22, interface22, requestevents22, answerevents22, out var supH22);
                    //Console.WriteLine("\tStates: {0}", supH22.Size);
                    //Console.WriteLine("\tTransitions: {0}", supH22.Transitions.Count());

                    //Console.WriteLine("--------------------------------------------------------------------------");

                    //Console.WriteLine("Synthesis H23");
                    //sintesesupLHISC(Plant23, Specification23, interface23, requestevents23, answerevents23, out var supH23);
                    //Console.WriteLine("\tStates: {0}", supH23.Size);
                    //Console.WriteLine("\tTransitions: {0}", supH23.Transitions.Count());

                    //Console.WriteLine("--------------------------------------------------------------------------");

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    interface1 = null;
                    interface21 = null;
                    interface22 = null;
                    interface23 = null;
                    Specification1 = null;
                    Specification21 = null;
                    Specification22 = null;
                    Specification23 = null;
                    Plant1 = null;
                    Plant21 = null;
                    Plant22 = null;
                    Plant23 = null;
                    K1 = null;
                    K21 = null;
                    K22 = null;
                    K23 = null;
                    supH11 = null;
                    supH21 = null;
                    supH22 = null;
                    supH23 = null;
                }

                Console.WriteLine("--------------------------------------------------------------------------");
            }           
        }
    }
}
