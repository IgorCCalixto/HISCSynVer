
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UltraDES;

namespace hierarquical
{
    internal class Program
    {

        private static void Hierarquical(out List<DeterministicFiniteAutomaton> plants, out List<DeterministicFiniteAutomaton> specs, out List<DeterministicFiniteAutomaton> interfaces, out List<DeterministicFiniteAutomaton> interfacesP)
        {

            //cria duas variaveis para conter uma lista de plantas e especificações respectivamente
            plants = new List<DeterministicFiniteAutomaton>();
            specs = new List<DeterministicFiniteAutomaton>();
            interfaces = new List<DeterministicFiniteAutomaton>();
            interfacesP = new List<DeterministicFiniteAutomaton>();

            //cria estados setando a quantidade a partir da função range e selecionando o que quer
            var s = Enumerable.Range(0, 51).Select(
                    k => new State(k.ToString(),
                        k == 0
                            ? Marking.Marked
                            : Marking.Unmarked))
                    .ToArray();
            // State s100 = new State("s100", Marking.Marked);
            s[49] = new State("49", Marking.Marked);
            s[50] = new State("50", Marking.Marked);

            //cria eventos 

            //GET023
            Event Start_GET023 = new Event("Start_GET023", Controllability.Controllable);//1
            Event Start_Copy_FS100_Position_Script_GET023 = new Event("Start_Copy_FS100_Position_Script_GET023", Controllability.Controllable);//3            
            Event End_Copy_FS100_Position_Script_GET023 = new Event("End_Copy_FS100_Position_Script_GET023", Controllability.Uncontrollable);//0
            Event Start_Go_To_Position_Pb1_GET023 = new Event("Start_Go_To_Position_Pb1_GET023", Controllability.Controllable);//5
            Event End_Go_To_Position_Pb1_GET023 = new Event("End_Go_To_Position_Pb1_GET023", Controllability.Uncontrollable);//2
            Event Start_JOB_GT023 = new Event("Start_JOB_GT023", Controllability.Controllable);//7
            Event End_JOB_GT023 = new Event("End_JOB_GT023", Controllability.Controllable);//9
            Event Start_Mov_GT023 = new Event("Start_Mov_GT023", Controllability.Controllable);//11
            Event End_Mov_GT023 = new Event("End_Mov_GT023", Controllability.Uncontrollable);//4
            Event Start_JOB_OGRIP_GT023 = new Event("Start_JOB_OGRIP_GT023", Controllability.Controllable);//13
            Event End_JOB_OGRIP_GT023 = new Event("End_JOB_OGRIP_GT023", Controllability.Controllable);//15
            Event Ver_In_Grvt_Sensor_On = new Event("Ver_In_Grvt_Sensor_On", Controllability.Controllable);//17
            Event In_Grvt_Sensor_On = new Event("In_Grvt_Sensor_On", Controllability.Uncontrollable);//6
            Event In_Grvt_Sensor_Off = new Event("In_Grvt_Sensor_Off", Controllability.Uncontrollable);//6
            Event Start_JOB_CGRIP_GT023 = new Event("Start_JOB_CGRIP_GT023", Controllability.Controllable);//19
            Event End_JOB_CGRIP_GT023 = new Event("End_JOB_CGRIP_GT023", Controllability.Controllable);//21
            Event Send_Start_GET023_to_MANAGER = new Event("Send_Start_GET023_to_MANAGER", Controllability.Controllable);//23

            //PUT022
            Event Start_PUT022 = new Event("Start_PUT022", Controllability.Controllable);//25
            Event Start_PREPARE_LATHE_LOAD = new Event("Start_PREPARE_LATHE_LOAD", Controllability.Controllable);//27
            Event Set_Lathe_Load = new Event("Set_Lathe_Load", Controllability.Controllable);//29
            Event End_PREPARE_LATHE_LOAD = new Event("End_PREPARE_LATHE_LOAD", Controllability.Controllable);//8
            Event Start_Go_To_Position_Pb1_PT022 = new Event("Start_Go_To_Position_Pb1_PT022", Controllability.Controllable);//31
            Event End_Go_To_Position_Pb1_PT022 = new Event("End_Go_To_Position_Pb1_PT022", Controllability.Uncontrollable);//10
            Event Ver_Lathe_PUT022 = new Event("Ver_Lathe_PUT022", Controllability.Controllable);//33
            Event Lathe_Load_PUT022 = new Event("Lathe_Load_PUT022", Controllability.Uncontrollable);//12
            Event Lathe_Unload_PUT022 = new Event("Lathe_Unload_PUT022", Controllability.Uncontrollable);//12
            Event Start_Copy_FS100_Position_Script_PT022 = new Event("Start_Copy_FS100_Position_Script_PT022", Controllability.Controllable);//35
            Event End_Copy_FS100_Position_Script_PT022 = new Event("End_Copy_FS100_Position_Script_PT022", Controllability.Uncontrollable);//14
            Event Start_JOB_PT022 = new Event("Start_JOB_PT022", Controllability.Controllable);//37
            Event End_JOB_PT022 = new Event("End_JOB_PT022", Controllability.Controllable);//39
            Event Start_CLOSE_LATHE_DOOR = new Event("Start_CLOSE_LATHE_DOOR", Controllability.Controllable);//41
            Event End_CLOSE_LATHE_DOOR = new Event("End_CLOSE_LATHE_DOOR", Controllability.Controllable);//43
            Event Send_Finish_PUT022_to_MANAGER = new Event("Send_Finish_PUT022_to_MANAGER", Controllability.Controllable);//45
            Event Send_End_PUT022_To_MANAGER = new Event("Send_End_PUT022_To_MANAGER", Controllability.Controllable);//47
            Event Start_Mov_PT022 = new Event("Start_Mov_PT022", Controllability.Controllable);//49
            Event End_Mov_PT022 = new Event("End_Mov_PT022", Controllability.Uncontrollable);//16
            Event Start_JOB_LODOOR_PT022 = new Event("Start_JOB_LODOOR_PT022", Controllability.Controllable);//51
            Event End_JOB_LODOOR_PT022 = new Event("End_JOB_LODOOR_PT022", Controllability.Controllable);//53
            Event Start_JOB_LOCHUCK_PT022 = new Event("Start_JOB_LOCHUCK_PT022", Controllability.Controllable);//55
            Event End_JOB_LOCHUCK_PT022 = new Event("End_JOB_LOCHUCK_PT022", Controllability.Controllable);//57
            Event Start_JOB_LCCHUCK_PT022 = new Event("Start_JOB_LCCHUCK_PT022", Controllability.Controllable);//59
            Event End_JOB_LCCHUCK_PT022 = new Event("End_JOB_LCCHUCK_PT022", Controllability.Controllable);//61
            Event Start_JOB_OGRIP_PT022 = new Event("Start_JOB_OGRIP_PT022", Controllability.Controllable);//63
            Event End_JOB_OGRIP_PT022 = new Event("End_JOB_OGRIP_PT022", Controllability.Controllable);//65


            //GET022
            Event Start_GET022 = new Event("Start_GET022", Controllability.Controllable);//67
            Event Start_PREPARE_LATHE_UNLOAD = new Event("Start_PREPARE_LATHE_UNLOAD", Controllability.Controllable);//69
            Event Set_Lathe_Unload = new Event("Set_Lathe_Unload", Controllability.Controllable);//71
            Event End_PREPARE_LATHE_UNLOAD = new Event("End_PREPARE_LATHE_UNLOAD", Controllability.Controllable);//18
            Event Start_Go_To_Position_Pb1_GET022 = new Event("Start_Go_To_Position_Pb1_GET022", Controllability.Controllable);//73
            Event End_Go_To_Position_Pb1_GET022 = new Event("End_Go_To_Position_Pb1_GET022", Controllability.Uncontrollable);//20
            Event Ver_Lathe_GET022 = new Event("Ver_Lathe_GET022", Controllability.Controllable);//33
            Event Lathe_Load_GET022 = new Event("Lathe_Load_GET022", Controllability.Uncontrollable);//12
            Event Lathe_Unload_GET022 = new Event("Lathe_Unload_GET022", Controllability.Uncontrollable);//12
            Event Start_Copy_FS100_Position_GET022 = new Event("Start_Copy_FS100_Position_GET022", Controllability.Controllable);//77
            Event End_Copy_FS100_Position_GET022 = new Event("End_Copy_FS100_Position_GET022", Controllability.Uncontrollable);//24
            Event Start_JOB_GT022 = new Event("Start_JOB_GT022", Controllability.Controllable);//79
            Event End_JOB_GT022 = new Event("End_JOB_GT022", Controllability.Controllable);//81
            Event Send_Start_GET022_to_MANAGER = new Event("Send_Start_GET022_to_MANAGER", Controllability.Controllable);//83
            Event Start_Mov_GT022 = new Event("Start_Mov_GT022", Controllability.Controllable);//85
            Event End_Mov_GT022 = new Event("End_Mov_GT022", Controllability.Uncontrollable);//26
            Event Start_JOB_LODOOR_GT022 = new Event("Start_JOB_LODOOR_GT022", Controllability.Controllable);//87
            Event End_JOB_LODOOR_GT022 = new Event("End_JOB_LODOOR_GT022", Controllability.Controllable);//89
            Event Start_JOB_OGRIP_GT022 = new Event("Start_JOB_OGRIP_GT022", Controllability.Controllable);//91
            Event End_JOB_OGRIP_GT022 = new Event("End_JOB_OGRIP_GT022", Controllability.Controllable);//93
            Event Start_JOB_CGRIP_GT022 = new Event("Start_JOB_CGRIP_GT022", Controllability.Controllable);//95
            Event End_JOB_CGRIP_GT022 = new Event("End_JOB_CGRIP_GT022", Controllability.Controllable);//97
            Event Start_JOB_LOCHUCK_GT022 = new Event("Start_JOB_LOCHUCK_GT022", Controllability.Controllable);//99
            Event End_JOB_LOCHUCK_GT022 = new Event("End_JOB_LOCHUCK_GT022", Controllability.Controllable);//101



            //PUT025
            Event Start_PUT025 = new Event("Start_PUT025", Controllability.Controllable);//103
            Event Put_Rack_Number = new Event("Put_Rack_Number", Controllability.Controllable);//105
            Event Start_PUT_RACK = new Event("Start_PUT_RACK", Controllability.Controllable);//107
            Event Send_End_PUT_RACK_To_MANAGER = new Event("Send_End_PUT_RACK_To_MANAGER", Controllability.Controllable);//109
            Event Start_Go_To_Position_Pb1_PUT_RACK = new Event("Start_Go_To_Position_Pb1_PUT_RACK", Controllability.Controllable);//111
            Event End_Go_To_Position_Pb1_PUT_RACK = new Event("End_Go_To_Position_Pb1_PUT_RACK", Controllability.Uncontrollable);//28
            Event Start_Copy_FS100_Position_Script_PUT_RACK = new Event("Start_Copy_FS100_Position_Script_PUT_RACK", Controllability.Controllable);//113
            Event End_Copy_FS100_Position_Script_PUT_RACK = new Event("End_Copy_FS100_Position_Script_PUT_RACK", Controllability.Uncontrollable);//30
            Event Start_JOB_PT_GENER = new Event("Start_JOB_PT_GENER", Controllability.Controllable);//115
            Event End_JOB_PT_GENER = new Event("End_JOB_PT_GENER", Controllability.Controllable);//117
            Event Send_Finish_PUT_RACK_To_MANAGER = new Event("Send_Finish_PUT_RACK_To_MANAGER", Controllability.Controllable);//119
            Event Start_Mov_PUT_RACK = new Event("Start_Mov_PUT_RACK", Controllability.Controllable);//121
            Event End_Mov_PUT_RACK = new Event("End_Mov_PUT_RACK", Controllability.Uncontrollable);//32
            Event Start_JOB_OGRIP_PT_GENER = new Event("Start_JOB_OGRIP_PT_GENER", Controllability.Controllable);//123
            Event End_JOB_OGRIP_PT_GENER = new Event("End_JOB_OGRIP_PT_GENER", Controllability.Controllable);//125

            //CNC

            Event Start_CYCLE_LATHE = new Event("Start_CYCLE_LATHE", Controllability.Controllable);//127
            Event O_Lathe_Cycle_Start_On = new Event("O_Lathe_Cycle_Start_On", Controllability.Controllable);//129
            Event O_Lathe_Cycle_Start_Off = new Event("O_Lathe_Cycle_Start_Off", Controllability.Controllable);//131
            Event Ver_I_Lathe_Cycle_Start_CYCLE_LATHE = new Event("Ver_I_Lathe_Cycle_Start_CYCLE_LATHE", Controllability.Controllable);//133
            Event I_Lathe_Cycle_Start_Off_CYCLE_LATHE = new Event("I_Lathe_Cycle_Start_Off_CYCLE_LATHE", Controllability.Uncontrollable);//34
            Event I_Lathe_Cycle_Start_On_CYCLE_LATHE = new Event("I_Lathe_Cycle_Start_On_CYCLE_LATHE", Controllability.Uncontrollable);//34
            Event End_CYCLE_LATHE = new Event("End_CYCLE_LATHE", Controllability.Controllable);//135
            Event Start_WAIT_CYCLE_END_LATHE = new Event("Start_WAIT_CYCLE_END_LATHE", Controllability.Controllable);//137
            Event Ver_I_Lathe_Cycle_Start_WAIT_CYCLE = new Event("Ver_I_Lathe_Cycle_Start_WAIT_CYCLE", Controllability.Controllable);//133
            Event I_Lathe_Cycle_Start_Off_WAIT_CYCLE = new Event("I_Lathe_Cycle_Start_Off_WAIT_CYCLE", Controllability.Uncontrollable);//34
            Event I_Lathe_Cycle_Start_On_WAIT_CYCLE = new Event("I_Lathe_Cycle_Start_On_WAIT_CYCLE", Controllability.Uncontrollable);//34
            Event Send_Endturn_to_CNC = new Event("Send_Endturn_to_CNC", Controllability.Controllable);//141
            Event End_WAIT_CYCLE_END_LATHE = new Event("End_WAIT_CYCLE_END_LATHE", Controllability.Controllable);//38
            Event CycleIni = new Event("CycleIni", Controllability.Uncontrollable);//34
            Event CycleFim = new Event("CycleFim", Controllability.Uncontrollable);//34
            //Event TimerH24 = new Event("TimerH24", Controllability.Controllable);//34


            //DOOR
            Event Ver_In_Lathe_Odoor = new Event("Ver_In_Lathe_Odoor", Controllability.Controllable);//143
            Event In_Lathe_Odoor_Off = new Event("In_Lathe_Odoor_Off", Controllability.Uncontrollable);//40
            //Event Ver_In_Ldoor_On = new Event("Ver_In_Ldoor_On", Controllability.Controllable);//145
            Event In_Lathe_Odoor_On = new Event("In_Lathe_Odoor_On", Controllability.Uncontrollable);//42
            Event Pulse_Out_9 = new Event("Pulse_Out_9", Controllability.Controllable);//147
            Event Ver_In_Lathe_Cdoor = new Event("Ver_In_Lathe_Cdoor", Controllability.Controllable);//149
            Event In_Lathe_Cdoor_Off = new Event("I_Lathe_Cdoor_Off", Controllability.Uncontrollable);//44
            //Event Ver_I_Lathe_Cdoor_On = new Event("Ver_I_Lathe_Cdoor_On", Controllability.Controllable);//151
            Event In_Lathe_Cdoor_On = new Event("In_Lathe_Cdoor_On", Controllability.Uncontrollable);//46
            Event O_Lathe_Cdoor_On = new Event("O_Lathe_Cdoor_On", Controllability.Controllable);//153
            Event O_Lathe_Cdoor_Off = new Event("O_Lathe_Cdoor_Off", Controllability.Controllable); //155

            //GRIP

            Event Out_Cgrip_On = new Event("Out_Cgrip_On", Controllability.Controllable);//157
            Event Out_Ogrip_On = new Event("Out_Ogrip_On", Controllability.Controllable);//159

            //CHUCK
            Event Ver_In_Lathe_Cchuck = new Event("Ver_In_Lathe_Cchuck", Controllability.Controllable);//161
            Event In_Lathe_Cchuck_Off = new Event("In_Lathe_Cchuck_Off", Controllability.Uncontrollable);//48
                                                                                                         // Event Ver_L_Cchuck_On = new Event("Ver_L_Cchuck_On", Controllability.Controllable);//163
            Event In_Lathe_Cchuck_On = new Event("In_Lathe_Cchuck_On", Controllability.Uncontrollable);//50
            Event Pulse_Out_12 = new Event("Pulse_Out_12", Controllability.Controllable);//165
            Event Ver_In_Lathe_Ochuck = new Event("Ver_In_Lathe_Ochuck", Controllability.Controllable);//167
            Event In_Lathe_Ochuck_Off = new Event("In_Lathe_Ochuck_Off", Controllability.Uncontrollable);//52
            //Event Ver_In_L_Ochuck_On = new Event("Ver_In_L_Ochuck_On", Controllability.Controllable);//169
            Event In_Lathe_Ochuck_On = new Event("In_Lathe_Ochuck_On", Controllability.Uncontrollable);//54
            Event Pulse_Out_11 = new Event("Pulse_Out_11", Controllability.Controllable);//171,

            /////////////////////////////////Plantas
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("Plants:");

            //////HIGHLEVEL

            ////H11

            //PCFs100H11 plants[0]
            var PCFs100 = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_Copy_FS100_Position_Script_GET023, s[1]),
                                    new Transition(s[1], End_Copy_FS100_Position_Script_GET023, s[0]),
                                    new Transition(s[0], Start_Copy_FS100_Position_Script_PT022, s[2]),
                                    new Transition(s[2], End_Copy_FS100_Position_Script_PT022, s[0]),
                                    new Transition(s[0], Start_Copy_FS100_Position_GET022, s[3]),
                                    new Transition(s[3], End_Copy_FS100_Position_GET022, s[0]),
                                    new Transition(s[0], Start_Copy_FS100_Position_Script_PUT_RACK, s[4]),
                                    new Transition(s[4], End_Copy_FS100_Position_Script_PUT_RACK, s[0])
                      }, s[0], "PCFs100");

            plants.Add(PCFs100);

            Console.WriteLine("\tAutomaton: {0}", PCFs100.ToString());
            Console.WriteLine("\tStates: {0}", PCFs100.Size);
            Console.WriteLine("\tTransitions: {0}", PCFs100.Transitions.Count());
            //PLANTFS100H11.drawSVGFigure("PLANTPOSITION", true);


            //PFs100H11 plants[1]
            var PFs100 = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_Mov_GT023, s[1]),
                                    new Transition(s[1], End_Mov_GT023, s[0]),
                                    new Transition(s[0], Start_Mov_PT022, s[2]),
                                    new Transition(s[2], End_Mov_PT022, s[0]),
                                    new Transition(s[0], Start_Mov_GT022, s[3]),
                                    new Transition(s[3], End_Mov_GT022, s[0]),
                                    new Transition(s[0], Start_Mov_PUT_RACK, s[4]),
                                    new Transition(s[4], End_Mov_PUT_RACK, s[0])
                      }, s[0], "PFs100");

            plants.Add(PFs100);

            Console.WriteLine("\tAutomaton: {0}", PFs100.ToString());
            Console.WriteLine("\tStates: {0}", PFs100.Size);
            Console.WriteLine("\tTransitions: {0}", PFs100.Transitions.Count());
            //PLANTFS100H11.drawSVGFigure("PLANTPOSITION", true);


            //PlantScorH11 plants[2]
            var PScor = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_GET023, s[1]),
                                    new Transition(s[1], Send_Start_GET023_to_MANAGER, s[0]),
                                    new Transition(s[0], Start_PUT022, s[2]),
                                    new Transition(s[2], Send_Finish_PUT022_to_MANAGER, s[3]),
                                    new Transition(s[3], Send_End_PUT022_To_MANAGER, s[0]),
                                    new Transition(s[0], Start_GET022, s[4]),
                                    new Transition(s[4], Send_Start_GET022_to_MANAGER, s[0]),
                                    new Transition(s[0], Start_PUT025, s[5]),
                                    new Transition(s[5], Put_Rack_Number, s[0]),
                                    new Transition(s[0], Start_PUT_RACK, s[6]),
                                    new Transition(s[6], Send_Finish_PUT_RACK_To_MANAGER, s[7]),
                                    new Transition(s[7], Send_End_PUT_RACK_To_MANAGER, s[0])
                      }, s[0], "PScor");

            plants.Add(PScor);

            Console.WriteLine("\tAutomaton: {0}", PScor.ToString());
            Console.WriteLine("\tStates: {0}", PScor.Size);
            Console.WriteLine("\tTransitions: {0}", PScor.Transitions.Count());
            //PlantScorH11.drawSVGFigure("PlantScorH21", true);

            //PInitialH11 plants[3]
            var PIniFs100 = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_JOB_GT023, s[1]),
                                    new Transition(s[1], End_JOB_GT023, s[0]),
                                    new Transition(s[0], Start_JOB_PT022, s[2]),
                                    new Transition(s[2], End_JOB_PT022, s[0]),
                                    new Transition(s[0], Start_JOB_GT022, s[3]),
                                    new Transition(s[3], End_JOB_GT022, s[0]),
                                    new Transition(s[0], Start_JOB_PT_GENER, s[4]),
                                    new Transition(s[4], End_JOB_PT_GENER, s[0])
                      }, s[0], "PIniFs100");

            plants.Add(PIniFs100);

            Console.WriteLine("\tAutomaton: {0}", PIniFs100.ToString());
            Console.WriteLine("\tStates: {0}", PIniFs100.Size);
            Console.WriteLine("\tTransitions: {0}", PIniFs100.Transitions.Count());
            // PlantInitialFs100H11.drawSVGFigure("PlantFs100H21", true);

            //PlantGrvtH11 plants[4]
            var PGrvt = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Ver_In_Grvt_Sensor_On, s[1]),
                                    new Transition(s[1], In_Grvt_Sensor_On, s[0]),
                                    new Transition(s[1], In_Grvt_Sensor_Off, s[0])
                      }, s[0], "PGrvt");

            plants.Add(PGrvt);

            Console.WriteLine("\tAutomaton: {0}", PGrvt.ToString());
            Console.WriteLine("\tStates: {0}", PGrvt.Size);
            Console.WriteLine("\tTransitions: {0}", PGrvt.Transitions.Count());


            //PlantVerLatheLoad plants[5]
            var PVerLathe = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Ver_Lathe_PUT022, s[1]),
                                    new Transition(s[1], Lathe_Unload_PUT022, s[0]),
                                    new Transition(s[1], Lathe_Load_PUT022, s[0]),
                                    new Transition(s[0], Ver_Lathe_GET022, s[2]),
                                    new Transition(s[2], Lathe_Unload_GET022, s[0]),
                                    new Transition(s[2], Lathe_Load_GET022, s[0])
                      }, s[0], "PVerLathe");

            plants.Add(PVerLathe);

            Console.WriteLine("\tAutomaton: {0}", PVerLathe.ToString());
            Console.WriteLine("\tStates: {0}", PVerLathe.Size);
            Console.WriteLine("\tTransitions: {0}", PVerLathe.Transitions.Count());

            //PlantLSBH11 plants[6]
            var PLSB = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_Go_To_Position_Pb1_GET023, s[1]),
                                    new Transition(s[1], End_Go_To_Position_Pb1_GET023, s[0]),
                                    new Transition(s[0], Start_Go_To_Position_Pb1_PT022, s[2]),
                                    new Transition(s[2], End_Go_To_Position_Pb1_PT022, s[0]),
                                    new Transition(s[0], Start_Go_To_Position_Pb1_GET022, s[3]),
                                    new Transition(s[3], End_Go_To_Position_Pb1_GET022, s[0]),
                                    new Transition(s[0], Start_Go_To_Position_Pb1_PUT_RACK, s[4]),
                                    new Transition(s[4], End_Go_To_Position_Pb1_PUT_RACK, s[0])
                      }, s[0], "PLSB");

            plants.Add(PLSB);

            Console.WriteLine("\tAutomaton: {0}", PLSB.ToString());
            Console.WriteLine("\tStates: {0}", PLSB.Size);
            Console.WriteLine("\tTransitions: {0}", PLSB.Transitions.Count());

            //PlantLATHEH11 plants[7]
            var PLathe = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Set_Lathe_Load, s[50]),
                                    new Transition(s[50], Set_Lathe_Unload, s[0]),
                                    new Transition(s[0], Set_Lathe_Unload, s[0]),
                                    new Transition(s[50], Set_Lathe_Load, s[50]),
                      }, s[0], "PLathe");

            plants.Add(PLathe);

            Console.WriteLine("\tAutomaton: {0}", PLathe.ToString());
            Console.WriteLine("\tStates: {0}", PLathe.Size);
            Console.WriteLine("\tTransitions: {0}", PLathe.Transitions.Count());

            //PlantPreparLatheH11 plants[8]
            var PPreparLathe = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_PREPARE_LATHE_LOAD, s[1]),
                                    new Transition(s[1], End_PREPARE_LATHE_LOAD, s[0]),
                                    new Transition(s[0], Start_PREPARE_LATHE_UNLOAD, s[2]),
                                    new Transition(s[2], End_PREPARE_LATHE_UNLOAD, s[0])
                      }, s[0], "PPreparLathe");

            plants.Add(PPreparLathe);

            Console.WriteLine("\tAutomaton: {0}", PPreparLathe.ToString());
            Console.WriteLine("\tStates: {0}", PPreparLathe.Size);
            Console.WriteLine("\tTransitions: {0}", PPreparLathe.Transitions.Count());



            //////low level

            ////H21

            //PlantDoorH21 plants[9]
            var PDoor = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Pulse_Out_9, s[50]),
                                    new Transition(s[50], O_Lathe_Cdoor_On, s[2]),
                                    new Transition(s[2], O_Lathe_Cdoor_Off, s[0]),
                                    new Transition(s[0], O_Lathe_Cdoor_On, s[3]),
                                    new Transition(s[3], O_Lathe_Cdoor_Off, s[0]),
                                    new Transition(s[0], In_Lathe_Cdoor_On, s[0]),
                                    new Transition(s[0], In_Lathe_Odoor_Off, s[0]),
                                    new Transition(s[50], Pulse_Out_9, s[50]),
                                    new Transition(s[50], In_Lathe_Odoor_On, s[50]),
                                    new Transition(s[50], In_Lathe_Cdoor_Off, s[50]),
                                    new Transition(s[2], In_Lathe_Odoor_On, s[2]),
                                    new Transition(s[2], In_Lathe_Cdoor_Off, s[2]),
                                    new Transition(s[3], In_Lathe_Cdoor_On, s[3]),
                                    new Transition(s[3], In_Lathe_Odoor_Off, s[3])
                      }, s[0], "PDoor");

            plants.Add(PDoor);

            Console.WriteLine("\tAutomaton: {0}", PDoor.ToString());
            Console.WriteLine("\tStates: {0}", PDoor.Size);
            Console.WriteLine("\tTransitions: {0}", PDoor.Transitions.Count());

            //PlantScorDoorH21 plants[10]
            var PScorDoor = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_JOB_LODOOR_PT022, s[0]),
                                    new Transition(s[0], End_JOB_LODOOR_PT022, s[0]),
                                    new Transition(s[0], Start_JOB_LODOOR_GT022, s[0]),
                                    new Transition(s[0], End_JOB_LODOOR_GT022, s[0]),
                                    new Transition(s[0], Start_CLOSE_LATHE_DOOR, s[0]),
                                    new Transition(s[0], End_CLOSE_LATHE_DOOR, s[0]),
                      }, s[0], "PScorDoor");

            plants.Add(PScorDoor);

            Console.WriteLine("\tAutomaton: {0}", PScorDoor.ToString());
            Console.WriteLine("\tStates: {0}", PScorDoor.Size);
            Console.WriteLine("\tTransitions: {0}", PScorDoor.Transitions.Count());

            //PlantVerLODOOR plants[11]
            var PVerLodoor = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Ver_In_Lathe_Odoor, s[1]),
                                    new Transition(s[1], In_Lathe_Odoor_On, s[0]),
                                    new Transition(s[1], In_Lathe_Odoor_Off, s[0]),

                      }, s[0], "PVerLodoor");

            plants.Add(PVerLodoor);

            Console.WriteLine("\tAutomaton: {0}", PVerLodoor.ToString());
            Console.WriteLine("\tStates: {0}", PVerLodoor.Size);
            Console.WriteLine("\tTransitions: {0}", PVerLodoor.Transitions.Count());

            //PlantVerLCDOORH21 plants[12]
            var PVerLCdoor = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Ver_In_Lathe_Cdoor, s[1]),
                                    new Transition(s[1], In_Lathe_Cdoor_Off, s[0]),
                                    new Transition(s[1], In_Lathe_Cdoor_On, s[0]),
                      }, s[0], "PVerLCdoor");

            plants.Add(PVerLCdoor);

            Console.WriteLine("\tAutomaton: {0}", PVerLCdoor.ToString());
            Console.WriteLine("\tStates: {0}", PVerLCdoor.Size);
            Console.WriteLine("\tTransitions: {0}", PVerLCdoor.Transitions.Count());



            ////H22


            //PlantScorGripH22 plants[13]
            var PScorGrip = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_JOB_OGRIP_GT023, s[0]),
                                    new Transition(s[0], End_JOB_OGRIP_GT023, s[0]),
                                    new Transition(s[0], Start_JOB_OGRIP_PT022, s[0]),
                                    new Transition(s[0], End_JOB_OGRIP_PT022, s[0]),
                                    new Transition(s[0], Start_JOB_OGRIP_GT022, s[0]),
                                    new Transition(s[0], End_JOB_OGRIP_GT022, s[0]),
                                    new Transition(s[0], Start_JOB_OGRIP_PT_GENER, s[0]),
                                    new Transition(s[0], End_JOB_OGRIP_PT_GENER, s[0]),
                                    new Transition(s[0], Start_JOB_CGRIP_GT023, s[0]),
                                    new Transition(s[0], End_JOB_CGRIP_GT023, s[0]),
                                    new Transition(s[0], Start_JOB_CGRIP_GT022, s[0]),
                                    new Transition(s[0], End_JOB_CGRIP_GT022, s[0]),
                      }, s[0], "PScorGrip");

            plants.Add(PScorGrip);

            Console.WriteLine("\tAutomaton: {0}", PScorGrip.ToString());
            Console.WriteLine("\tStates: {0}", PScorGrip.Size);
            Console.WriteLine("\tTransitions: {0}", PScorGrip.Transitions.Count());

            //PlantGripH22 plants[14]
            var PGrip = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Out_Ogrip_On, s[50]),
                                    new Transition(s[50], Out_Cgrip_On, s[0]),
                                    new Transition(s[0], Out_Cgrip_On, s[0]),
                                    new Transition(s[50], Out_Ogrip_On, s[50]),
                      }, s[0], "PGrip");

            plants.Add(PGrip);

            Console.WriteLine("\tAutomaton: {0}", PGrip.ToString());
            Console.WriteLine("\tStates: {0}", PGrip.Size);
            Console.WriteLine("\tTransitions: {0}", PGrip.Transitions.Count());


            ////H23


            //PlantScorChuckH23 plants[15]
            var PScorChuck = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_JOB_LOCHUCK_PT022, s[0]),
                                    new Transition(s[0], End_JOB_LOCHUCK_PT022, s[0]),
                                    new Transition(s[0], Start_JOB_LOCHUCK_GT022, s[0]),
                                    new Transition(s[0], End_JOB_LOCHUCK_GT022, s[0]),
                                    new Transition(s[0], Start_JOB_LCCHUCK_PT022, s[0]),
                                    new Transition(s[0], End_JOB_LCCHUCK_PT022, s[0]),
                      }, s[0], "PScorChuck");

            plants.Add(PScorChuck);

            Console.WriteLine("\tAutomaton: {0}", PScorChuck.ToString());
            Console.WriteLine("\tStates: {0}", PScorChuck.Size);
            Console.WriteLine("\tTransitions: {0}", PScorChuck.Transitions.Count());

            //PlantChuckH33 plants[16]
            var PChuck = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Pulse_Out_12, s[0]),
                                    new Transition(s[0], In_Lathe_Cchuck_On, s[0]),
                                    new Transition(s[0], In_Lathe_Ochuck_Off, s[0]),
                                    new Transition(s[0], Pulse_Out_11, s[49]),
                                    new Transition(s[49], Pulse_Out_11, s[49]),
                                    new Transition(s[49], In_Lathe_Ochuck_On, s[49]),
                                    new Transition(s[49], In_Lathe_Cchuck_Off, s[49]),
                                    new Transition(s[49], Pulse_Out_12 , s[0])
                      }, s[0], "PChuck");

            plants.Add(PChuck);

            Console.WriteLine("\tAutomaton: {0}", PChuck.ToString());
            Console.WriteLine("\tStates: {0}", PChuck.Size);
            Console.WriteLine("\tTransitions: {0}", PChuck.Transitions.Count());

            //PlantVerLCchuckH23 plants[17]
            var PVerLCchuck = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Ver_In_Lathe_Cchuck, s[1]),
                                    new Transition(s[1], In_Lathe_Cchuck_On, s[0]),
                                    new Transition(s[1], In_Lathe_Cchuck_Off, s[0]),

                      }, s[0], "PVerLCchuck");

            plants.Add(PVerLCchuck);

            Console.WriteLine("\tAutomaton: {0}", PVerLCchuck.ToString());
            Console.WriteLine("\tStates: {0}", PVerLCchuck.Size);
            Console.WriteLine("\tTransitions: {0}", PVerLCchuck.Transitions.Count());


            //PlantVerLOchuckH23 plants[18]
            var PVerLOchuck = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Ver_In_Lathe_Ochuck, s[1]),
                                    new Transition(s[1], In_Lathe_Ochuck_Off, s[0]),
                                    new Transition(s[1], In_Lathe_Ochuck_On, s[0])
                      }, s[0], "PVerLOchuck");

            plants.Add(PVerLOchuck);

            Console.WriteLine("\tAutomaton: {0}", PVerLOchuck.ToString());
            Console.WriteLine("\tStates: {0}", PVerLOchuck.Size);
            Console.WriteLine("\tTransitions: {0}", PVerLOchuck.Transitions.Count());


            //H24

            //PLatheOperationH24 plants[19]
            var PLatheOper = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], O_Lathe_Cycle_Start_On, s[1]),
                                    new Transition(s[1], O_Lathe_Cycle_Start_Off, s[2]),
                                    new Transition(s[2],CycleIni, s[3]),
                                    new Transition(s[3],I_Lathe_Cycle_Start_Off_CYCLE_LATHE, s[4]),
                                    new Transition(s[3],I_Lathe_Cycle_Start_Off_WAIT_CYCLE, s[4]),
                                    new Transition(s[4], CycleFim, s[0]),
                                    new Transition(s[0],I_Lathe_Cycle_Start_On_CYCLE_LATHE, s[0]),
                                    new Transition(s[0],I_Lathe_Cycle_Start_On_WAIT_CYCLE, s[0]),
                                    new Transition(s[1],I_Lathe_Cycle_Start_On_CYCLE_LATHE, s[1]),
                                    new Transition(s[1],I_Lathe_Cycle_Start_On_WAIT_CYCLE, s[1]),
                                    new Transition(s[2],I_Lathe_Cycle_Start_On_CYCLE_LATHE, s[2]),
                                    new Transition(s[2],I_Lathe_Cycle_Start_On_WAIT_CYCLE, s[2]),
                                    new Transition(s[4],I_Lathe_Cycle_Start_Off_CYCLE_LATHE, s[4]),
                                    new Transition(s[4],I_Lathe_Cycle_Start_Off_WAIT_CYCLE, s[4])
                      }, s[0], "PLatheOper");

            plants.Add(PLatheOper);

            Console.WriteLine("\tAutomaton: {0}", PLatheOper.ToString());
            Console.WriteLine("\tStates: {0}", PLatheOper.Size);
            Console.WriteLine("\tTransitions: {0}", PLatheOper.Transitions.Count());

            // PIniScorH24 plants[20]
            var PIniScor = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_CYCLE_LATHE, s[0]),
                                    new Transition(s[0], End_CYCLE_LATHE, s[0])
                      }, s[0], "PIniScor");

            plants.Add(PIniScor);

            Console.WriteLine("\tAutomaton: {0}", PIniScor.ToString());
            Console.WriteLine("\tStates: {0}", PIniScor.Size);
            Console.WriteLine("\tTransitions: {0}", PIniScor.Transitions.Count());

            // PIniwaitH24 plants[21]
            var PIniWait = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_WAIT_CYCLE_END_LATHE, s[1]),
                                    new Transition(s[1], Send_Endturn_to_CNC, s[2]),
                                    new Transition(s[2], End_WAIT_CYCLE_END_LATHE, s[0])
                      }, s[0], "PIniWait");

            plants.Add(PIniWait);

            Console.WriteLine("\tAutomaton: {0}", PIniWait.ToString());
            Console.WriteLine("\tStates: {0}", PIniWait.Size);
            Console.WriteLine("\tTransitions: {0}", PIniWait.Transitions.Count());

            //PVerLatheLoad plants[22]
            var PVerCycle = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Ver_I_Lathe_Cycle_Start_CYCLE_LATHE, s[1]),
                                    new Transition(s[1], I_Lathe_Cycle_Start_Off_CYCLE_LATHE, s[0]),
                                    new Transition(s[1], I_Lathe_Cycle_Start_On_CYCLE_LATHE, s[0]),
                                    new Transition(s[0], Ver_I_Lathe_Cycle_Start_WAIT_CYCLE, s[2]),
                                    new Transition(s[2], I_Lathe_Cycle_Start_Off_WAIT_CYCLE, s[0]),
                                    new Transition(s[2], I_Lathe_Cycle_Start_On_WAIT_CYCLE, s[0])
                      }, s[0], "PVerCycle");

            plants.Add(PVerCycle);

            Console.WriteLine("\tAutomaton: {0}", PVerCycle.ToString());
            Console.WriteLine("\tStates: {0}", PVerCycle.Size);
            Console.WriteLine("\tTransitions: {0}", PVerCycle.Transitions.Count());

            ///////////////////// Plantas Modular///////////////////////////////////////
            Console.WriteLine("Modular Plants:");
            //PlantScorDoorMM plants[23]
            var PScorDoorMM = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_JOB_LODOOR_PT022, s[1]),
                                    new Transition(s[1], End_JOB_LODOOR_PT022, s[0]),
                                    new Transition(s[0], Start_JOB_LODOOR_GT022, s[2]),
                                    new Transition(s[2], End_JOB_LODOOR_GT022, s[0]),
                                    new Transition(s[0], Start_CLOSE_LATHE_DOOR, s[3]),
                                    new Transition(s[3], End_CLOSE_LATHE_DOOR, s[0]),
                      }, s[0], "PScorDoorMM");

            plants.Add(PScorDoorMM);

            Console.WriteLine("\tAutomaton: {0}", PScorDoorMM.ToString());
            Console.WriteLine("\tStates: {0}", PScorDoorMM.Size);
            Console.WriteLine("\tTransitions: {0}", PScorDoorMM.Transitions.Count());

            //PlantScorGripMM plants[24]
            var PScorGripMM = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_JOB_OGRIP_GT023, s[1]),
                                    new Transition(s[1], End_JOB_OGRIP_GT023, s[0]),
                                    new Transition(s[0], Start_JOB_OGRIP_PT022, s[2]),
                                    new Transition(s[2], End_JOB_OGRIP_PT022, s[0]),
                                    new Transition(s[0], Start_JOB_OGRIP_GT022, s[3]),
                                    new Transition(s[3], End_JOB_OGRIP_GT022, s[0]),
                                    new Transition(s[0], Start_JOB_OGRIP_PT_GENER, s[4]),
                                    new Transition(s[4], End_JOB_OGRIP_PT_GENER, s[0]),
                                    new Transition(s[0], Start_JOB_CGRIP_GT023, s[5]),
                                    new Transition(s[5], End_JOB_CGRIP_GT023, s[0]),
                                    new Transition(s[0], Start_JOB_CGRIP_GT022, s[6]),
                                    new Transition(s[6], End_JOB_CGRIP_GT022, s[0]),
                      }, s[0], "PScorGripMM");

            plants.Add(PScorGripMM);

            Console.WriteLine("\tAutomaton: {0}", PScorGripMM.ToString());
            Console.WriteLine("\tStates: {0}", PScorGripMM.Size);
            Console.WriteLine("\tTransitions: {0}", PScorGripMM.Transitions.Count());



            //PlantScorChuckH23 plants[25]
            var PScorChuckMM = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_JOB_LOCHUCK_PT022, s[1]),
                                    new Transition(s[1], End_JOB_LOCHUCK_PT022, s[0]),
                                    new Transition(s[0], Start_JOB_LOCHUCK_GT022, s[2]),
                                    new Transition(s[2], End_JOB_LOCHUCK_GT022, s[0]),
                                    new Transition(s[0], Start_JOB_LCCHUCK_PT022, s[3]),
                                    new Transition(s[3], End_JOB_LCCHUCK_PT022, s[0]),
                      }, s[0], "PScorChuckMM");

            plants.Add(PScorChuckMM);

            Console.WriteLine("\tAutomaton: {0}", PScorChuckMM.ToString());
            Console.WriteLine("\tStates: {0}", PScorChuckMM.Size);
            Console.WriteLine("\tTransitions: {0}", PScorChuckMM.Transitions.Count());


            // PIniScorMM plants[26]
            var PIniScorMM = new DeterministicFiniteAutomaton(new[]
                      {
                                    new Transition(s[0], Start_CYCLE_LATHE, s[1]),
                                    new Transition(s[1], End_CYCLE_LATHE, s[0])
                      }, s[0], "PIniScorMM");

            plants.Add(PIniScorMM);

            Console.WriteLine("\tAutomaton: {0}", PIniScorMM.ToString());
            Console.WriteLine("\tStates: {0}", PIniScorMM.Size);
            Console.WriteLine("\tTransitions: {0}", PIniScorMM.Transitions.Count());

            ////////////////////////////especificações
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("Specifications:");

            //HIGHLEVEL

            //H21

            //GET023 specs[0]
            var GET023 = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_GET023, s[1]),
                            new Transition(s[1], Start_Copy_FS100_Position_Script_GET023, s[2]),
                            new Transition(s[2], End_Copy_FS100_Position_Script_GET023, s[3]),
                            new Transition(s[3], Start_Go_To_Position_Pb1_GET023, s[4]),
                            new Transition(s[4], End_Go_To_Position_Pb1_GET023, s[5]),
                            new Transition(s[5], Start_JOB_GT023, s[6]),
                            new Transition(s[6], End_JOB_GT023, s[7]),
                            new Transition(s[7], Send_Start_GET023_to_MANAGER, s[0]),
                        }, s[0], "GET023");

            specs.Add(GET023);

            Console.WriteLine("\tAutomaton: {0}", GET023.ToString());
            Console.WriteLine("\tStates: {0}", GET023.Size);
            Console.WriteLine("\tTransitions: {0}", GET023.Transitions.Count());

            //JOBGT023 specs[1]
            var JOBGT023 = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_JOB_GT023, s[1]),
                            new Transition(s[1], Start_Mov_GT023, s[2]),
                            new Transition(s[2], End_Mov_GT023, s[3]),
                            new Transition(s[3], Start_JOB_OGRIP_GT023, s[4]),
                            new Transition(s[4], End_JOB_OGRIP_GT023, s[5]),
                            new Transition(s[5], Ver_In_Grvt_Sensor_On, s[6]),
                            new Transition(s[6], In_Grvt_Sensor_Off, s[5]),
                            new Transition(s[6], In_Grvt_Sensor_On, s[7]),
                            new Transition(s[7], Start_Mov_GT023, s[8]),
                            new Transition(s[8], End_Mov_GT023, s[9]),
                            new Transition(s[9], Start_JOB_CGRIP_GT023, s[10]),
                            new Transition(s[10], End_JOB_CGRIP_GT023, s[11]),
                            new Transition(s[11], Start_Mov_GT023, s[12]),
                            new Transition(s[12], End_Mov_GT023, s[13]),
                            new Transition(s[13], End_JOB_GT023, s[0]),
                        }, s[0], "JOBGT023");

            specs.Add(JOBGT023);

            Console.WriteLine("\tAutomaton: {0}", JOBGT023.ToString());
            Console.WriteLine("\tStates: {0}", JOBGT023.Size);
            Console.WriteLine("\tTransitions: {0}", JOBGT023.Transitions.Count());

            //PUT022 specs[2]
            var PUT022 = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_PUT022, s[1]),
                            new Transition(s[1], Start_PREPARE_LATHE_LOAD, s[2]),
                            new Transition(s[2], End_PREPARE_LATHE_LOAD, s[3]),
                            new Transition(s[3], Start_Go_To_Position_Pb1_PT022, s[4]),
                            new Transition(s[4], End_Go_To_Position_Pb1_PT022, s[5]),
                            new Transition(s[5], Ver_Lathe_PUT022, s[6]),
                            new Transition(s[6], Lathe_Load_PUT022, s[7]),
                            new Transition(s[6], Lathe_Unload_PUT022, s[5]),
                            new Transition(s[7], Start_Copy_FS100_Position_Script_PT022, s[8]),
                            new Transition(s[8], End_Copy_FS100_Position_Script_PT022, s[9]),
                            new Transition(s[9], Start_JOB_PT022, s[10]),
                            new Transition(s[10], End_JOB_PT022, s[11]),
                            new Transition(s[11], Start_CLOSE_LATHE_DOOR, s[12]),
                            new Transition(s[12], End_CLOSE_LATHE_DOOR, s[13]),
                            new Transition(s[13], Send_Finish_PUT022_to_MANAGER, s[14]),
                            new Transition(s[14], Start_Go_To_Position_Pb1_PT022, s[15]),
                            new Transition(s[15], End_Go_To_Position_Pb1_PT022, s[16]),
                            new Transition(s[16], Send_End_PUT022_To_MANAGER, s[0]),
                        }, s[0], "PUT022");

            specs.Add(PUT022);

            Console.WriteLine("\tAutomaton: {0}", PUT022.ToString());
            Console.WriteLine("\tStates: {0}", PUT022.Size);
            Console.WriteLine("\tTransitions: {0}", PUT022.Transitions.Count());

            //JOBPT022 specs[3]
            var JOBPT022 = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_JOB_PT022, s[1]),
                            new Transition(s[1], Start_Mov_PT022, s[2]),
                            new Transition(s[2], End_Mov_PT022, s[3]),
                            new Transition(s[3], Start_JOB_LODOOR_PT022, s[4]),
                            new Transition(s[4], End_JOB_LODOOR_PT022, s[5]),
                            new Transition(s[5], Start_JOB_LOCHUCK_PT022, s[6]),
                            new Transition(s[6], End_JOB_LOCHUCK_PT022, s[7]),
                            new Transition(s[7], Start_Mov_PT022, s[8]),
                            new Transition(s[8], End_Mov_PT022, s[9]),
                            new Transition(s[9], Start_JOB_LCCHUCK_PT022, s[10]),
                            new Transition(s[10], End_JOB_LCCHUCK_PT022, s[11]),
                            new Transition(s[11], Start_JOB_OGRIP_PT022, s[12]),
                            new Transition(s[12], End_JOB_OGRIP_PT022, s[13]),
                            new Transition(s[13], Start_Mov_PT022, s[14]),
                            new Transition(s[14], End_Mov_PT022, s[15]),
                            new Transition(s[15], End_JOB_PT022, s[0]),
                        }, s[0], "JOBPT022");

            specs.Add(JOBPT022);

            Console.WriteLine("\tAutomaton: {0}", JOBPT022.ToString());
            Console.WriteLine("\tStates: {0}", JOBPT022.Size);
            Console.WriteLine("\tTransitions: {0}", JOBPT022.Transitions.Count());

            //GET022 specs[4]
            var GET022 = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_GET022, s[1]),
                            new Transition(s[1], Start_PREPARE_LATHE_UNLOAD, s[2]),
                            new Transition(s[2], End_PREPARE_LATHE_UNLOAD, s[3]),
                            new Transition(s[3], Start_Go_To_Position_Pb1_GET022, s[4]),
                            new Transition(s[4], End_Go_To_Position_Pb1_GET022, s[5]),
                            new Transition(s[5], Ver_Lathe_GET022, s[6]),
                            new Transition(s[6], Lathe_Unload_GET022, s[7]),
                            new Transition(s[6], Lathe_Load_GET022, s[5]),
                            new Transition(s[7], Start_Copy_FS100_Position_GET022, s[8]),
                            new Transition(s[8], End_Copy_FS100_Position_GET022, s[9]),
                            new Transition(s[9], Start_JOB_GT022, s[10]),
                            new Transition(s[10], End_JOB_GT022, s[11]),
                            new Transition(s[11], Send_Start_GET022_to_MANAGER, s[0]),
                        }, s[0], "GET022");

            specs.Add(GET022);

            Console.WriteLine("\tAutomaton: {0}", GET022.ToString());
            Console.WriteLine("\tStates: {0}", GET022.Size);
            Console.WriteLine("\tTransitions: {0}", GET022.Transitions.Count());

            //JOBGT022  specs[5]
            var JOBGT022 = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_JOB_GT022, s[1]),
                            new Transition(s[1], Start_Mov_GT022, s[2]),
                            new Transition(s[2], End_Mov_GT022, s[3]),
                            new Transition(s[3], Start_JOB_LODOOR_GT022, s[4]),
                            new Transition(s[4], End_JOB_LODOOR_GT022, s[5]),
                            new Transition(s[5], Start_Mov_GT022, s[6]),
                            new Transition(s[6], End_Mov_GT022, s[7]),
                            new Transition(s[7], Start_JOB_OGRIP_GT022, s[8]),
                            new Transition(s[8], End_JOB_OGRIP_GT022, s[9]),
                            new Transition(s[9], Start_Mov_GT022, s[10]),
                            new Transition(s[10], End_Mov_GT022, s[11]),
                            new Transition(s[11], Start_JOB_CGRIP_GT022, s[12]),
                            new Transition(s[12], End_JOB_CGRIP_GT022, s[13]),
                            new Transition(s[13], Start_JOB_LOCHUCK_GT022, s[14]),
                            new Transition(s[14], End_JOB_LOCHUCK_GT022, s[15]),
                            new Transition(s[15], Start_Mov_GT022, s[16]),
                            new Transition(s[16], End_Mov_GT022, s[17]),
                            new Transition(s[17], End_JOB_GT022, s[0]),
                        }, s[0], "JOBGT022");

            specs.Add(JOBGT022);

            Console.WriteLine("\tAutomaton: {0}", JOBGT022.ToString());
            Console.WriteLine("\tStates: {0}", JOBGT022.Size);
            Console.WriteLine("\tTransitions: {0}", JOBGT022.Transitions.Count());

            //PUT025 specs[6]
            var PUT025 = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_PUT025, s[1]),
                            new Transition(s[1], Put_Rack_Number, s[2]),
                            new Transition(s[2], Start_PUT_RACK, s[3]),
                            new Transition(s[3], Send_End_PUT_RACK_To_MANAGER, s[0]),
                        }, s[0], "PUT025");

            specs.Add(PUT025);

            Console.WriteLine("\tAutomaton: {0}", PUT025.ToString());
            Console.WriteLine("\tStates: {0}", PUT025.Size);
            Console.WriteLine("\tTransitions: {0}", PUT025.Transitions.Count());

            //PUTRACK specs[7]
            var PUTRACK = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_PUT_RACK, s[1]),
                            new Transition(s[1], Start_Go_To_Position_Pb1_PUT_RACK, s[2]),
                            new Transition(s[2], End_Go_To_Position_Pb1_PUT_RACK, s[3]),
                            new Transition(s[3], Start_Copy_FS100_Position_Script_PUT_RACK, s[4]),
                            new Transition(s[4], End_Copy_FS100_Position_Script_PUT_RACK, s[5]),
                            new Transition(s[5], Start_JOB_PT_GENER, s[6]),
                            new Transition(s[6], End_JOB_PT_GENER, s[7]),
                            new Transition(s[7], Send_Finish_PUT_RACK_To_MANAGER, s[8]),
                            new Transition(s[8], Send_End_PUT_RACK_To_MANAGER, s[0]),
                        }, s[0], "PUTRACK");

            specs.Add(PUTRACK);

            Console.WriteLine("\tAutomaton: {0}", PUTRACK.ToString());
            Console.WriteLine("\tStates: {0}", PUTRACK.Size);
            Console.WriteLine("\tTransitions: {0}", PUTRACK.Transitions.Count());

            //PTGENER specs[8]
            var PTGENER = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_JOB_PT_GENER, s[1]),
                            new Transition(s[1], Start_Mov_PUT_RACK, s[2]),
                            new Transition(s[2], End_Mov_PUT_RACK, s[3]),
                            new Transition(s[3], Start_JOB_OGRIP_PT_GENER, s[4]),
                            new Transition(s[4], End_JOB_OGRIP_PT_GENER, s[5]),
                            new Transition(s[5], Start_Mov_PUT_RACK, s[6]),
                            new Transition(s[6], End_Mov_PUT_RACK, s[7]),
                            new Transition(s[7], End_JOB_PT_GENER, s[0]),
                        }, s[0], "PTGENER");

            specs.Add(PTGENER);

            Console.WriteLine("\tAutomaton: {0}", PTGENER.ToString());
            Console.WriteLine("\tStates: {0}", PTGENER.Size);
            Console.WriteLine("\tTransitions: {0}", PTGENER.Transitions.Count());

            //Manager specs[9]
            var Manager = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_GET023, s[1]),
                            new Transition(s[1], Send_Start_GET023_to_MANAGER, s[2]),
                            new Transition(s[2], Start_PUT022, s[3]),
                            new Transition(s[3], Send_End_PUT022_To_MANAGER, s[4]),
                            new Transition(s[4], Start_CYCLE_LATHE, s[5]),
                            new Transition(s[5], End_CYCLE_LATHE, s[6]),
                            new Transition(s[6], Start_GET022, s[7]),
                            new Transition(s[7], Send_Start_GET022_to_MANAGER, s[8]),
                            new Transition(s[8], Start_PUT025, s[9]),
                            new Transition(s[9], Send_End_PUT_RACK_To_MANAGER, s[0]),
                        }, s[0], "Manager");

            specs.Add(Manager);

            Console.WriteLine("\tAutomaton: {0}", Manager.ToString());
            Console.WriteLine("\tStates: {0}", Manager.Size);
            Console.WriteLine("\tTransitions: {0}", Manager.Transitions.Count());

            //PLATHELO specs[10]
            var PLATHELO = new DeterministicFiniteAutomaton(new[]
           {
                            new Transition(s[0], Start_PREPARE_LATHE_LOAD, s[1]),
                            new Transition(s[1], Set_Lathe_Load, s[2]),
                            new Transition(s[2], End_PREPARE_LATHE_LOAD, s[0]),
                        }, s[0], "PLATHELO");

            specs.Add(PLATHELO);

            Console.WriteLine("\tAutomaton: {0}", PLATHELO.ToString());
            Console.WriteLine("\tStates: {0}", PLATHELO.Size);
            Console.WriteLine("\tTransitions: {0}", PLATHELO.Transitions.Count());


            //PLATHEUN specs[11]
            var PLATHEUN = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_PREPARE_LATHE_UNLOAD, s[1]),
                            new Transition(s[1], Set_Lathe_Unload, s[2]),
                            new Transition(s[2], End_PREPARE_LATHE_UNLOAD, s[0]),
                        }, s[0], "PLATHEUN");

            specs.Add(PLATHEUN);

            Console.WriteLine("\tAutomaton: {0}", PLATHEUN.ToString());
            Console.WriteLine("\tStates: {0}", PLATHEUN.Size);
            Console.WriteLine("\tTransitions: {0}", PLATHEUN.Transitions.Count());



            /////LOW LEVEL

            ////H21

            //CLLATHEDOOR specs[12]
            var CLLATHEDOOR = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_CLOSE_LATHE_DOOR, s[1]),
                            new Transition(s[1], Ver_In_Lathe_Cdoor, s[2]),
                            new Transition(s[2], In_Lathe_Cdoor_Off, s[3]),
                            new Transition(s[3], O_Lathe_Cdoor_On, s[4]),
                            new Transition(s[4], O_Lathe_Cdoor_Off, s[5]),
                            new Transition(s[5], Ver_In_Lathe_Cdoor, s[6]),
                            new Transition(s[6], In_Lathe_Cdoor_On, s[7]),
                            new Transition(s[7], End_CLOSE_LATHE_DOOR, s[0]),
                            new Transition(s[2], In_Lathe_Cdoor_On, s[7]),
                            new Transition(s[6], In_Lathe_Cdoor_Off, s[3]),
                        }, s[0], "CLLATHEDOOR");

            specs.Add(CLLATHEDOOR);

            Console.WriteLine("\tAutomaton: {0}", CLLATHEDOOR.ToString());
            Console.WriteLine("\tStates: {0}", CLLATHEDOOR.Size);
            Console.WriteLine("\tTransitions: {0}", CLLATHEDOOR.Transitions.Count());

            //LODOOR specs[13]
            var LODOOR = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_JOB_LODOOR_GT022, s[1]),
                            new Transition(s[0], Start_JOB_LODOOR_PT022, s[1]),
                            new Transition(s[1], Ver_In_Lathe_Odoor, s[2]),
                            new Transition(s[2], In_Lathe_Odoor_Off, s[3]),
                            new Transition(s[3], Pulse_Out_9, s[4]),
                            new Transition(s[4], Ver_In_Lathe_Odoor, s[5]),
                            new Transition(s[5], In_Lathe_Odoor_On, s[6]),
                            new Transition(s[6], End_JOB_LODOOR_GT022, s[0]),
                            new Transition(s[6], End_JOB_LODOOR_PT022, s[0]),
                            new Transition(s[2], In_Lathe_Odoor_On, s[6]),
                            new Transition(s[5], In_Lathe_Odoor_Off, s[3]),
                        }, s[0], "LODOOR");

            specs.Add(LODOOR);

            Console.WriteLine("\tAutomaton: {0}", LODOOR.ToString());
            Console.WriteLine("\tStates: {0}", LODOOR.Size);
            Console.WriteLine("\tTransitions: {0}", LODOOR.Transitions.Count());

            ////H22

            //OGRIP specs[14]
            var OGRIP = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_JOB_OGRIP_GT022, s[1]),
                            new Transition(s[0], Start_JOB_OGRIP_GT023, s[1]),
                            new Transition(s[0], Start_JOB_OGRIP_PT022, s[1]),
                            new Transition(s[0], Start_JOB_OGRIP_PT_GENER, s[1]),
                            new Transition(s[1], Out_Ogrip_On, s[2]),
                            new Transition(s[2], End_JOB_OGRIP_GT022, s[0]),
                            new Transition(s[2], End_JOB_OGRIP_GT023, s[0]),
                            new Transition(s[2], End_JOB_OGRIP_PT022, s[0]),
                            new Transition(s[2], End_JOB_OGRIP_PT_GENER, s[0]),
                        }, s[0], "OGRIP");

            specs.Add(OGRIP);

            Console.WriteLine("\tAutomaton: {0}", OGRIP.ToString());
            Console.WriteLine("\tStates: {0}", OGRIP.Size);
            Console.WriteLine("\tTransitions: {0}", OGRIP.Transitions.Count());

            //CGRIP specs[15]
            var CGRIP = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_JOB_CGRIP_GT023, s[1]),
                            new Transition(s[0], Start_JOB_CGRIP_GT022, s[1]),
                            new Transition(s[1], Out_Cgrip_On, s[2]),
                            new Transition(s[2], End_JOB_CGRIP_GT023, s[0]),
                            new Transition(s[2], End_JOB_CGRIP_GT022, s[0]),
                        }, s[0], "CGRIP");

            specs.Add(CGRIP);

            Console.WriteLine("\tAutomaton: {0}", CGRIP.ToString());
            Console.WriteLine("\tStates: {0}", CGRIP.Size);
            Console.WriteLine("\tTransitions: {0}", CGRIP.Transitions.Count());


            ////H23

            //LOCHUCK specs[16]
            var LOCHUCK = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_JOB_LOCHUCK_GT022, s[1]),
                            new Transition(s[0], Start_JOB_LOCHUCK_PT022, s[1]),
                            new Transition(s[1], Ver_In_Lathe_Ochuck, s[2]),
                            new Transition(s[2], In_Lathe_Ochuck_Off, s[3]),
                            new Transition(s[3], Pulse_Out_11, s[4]),
                            new Transition(s[4], Ver_In_Lathe_Ochuck, s[5]),
                            new Transition(s[5], In_Lathe_Ochuck_On, s[6]),
                            new Transition(s[6], End_JOB_LOCHUCK_GT022, s[0]),
                            new Transition(s[6], End_JOB_LOCHUCK_PT022, s[0]),
                            new Transition(s[2], In_Lathe_Ochuck_On, s[6]),
                            new Transition(s[5], In_Lathe_Ochuck_Off, s[3]),
                        }, s[0], "LOCHUCK");

            specs.Add(LOCHUCK);

            Console.WriteLine("\tAutomaton: {0}", LOCHUCK.ToString());
            Console.WriteLine("\tStates: {0}", LOCHUCK.Size);
            Console.WriteLine("\tTransitions: {0}", LOCHUCK.Transitions.Count());

            //LCCHUCK specs[17]
            var LCCHUCK = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_JOB_LCCHUCK_PT022, s[1]),
                            new Transition(s[1], Ver_In_Lathe_Cchuck, s[2]),
                            new Transition(s[2], In_Lathe_Cchuck_Off, s[3]),
                            new Transition(s[3], Pulse_Out_12, s[4]),
                            new Transition(s[4], Ver_In_Lathe_Cchuck, s[5]),
                            new Transition(s[5], In_Lathe_Cchuck_On, s[6]),
                            new Transition(s[6], End_JOB_LCCHUCK_PT022, s[0]),
                            new Transition(s[2], In_Lathe_Cchuck_On, s[6]),
                            new Transition(s[5], In_Lathe_Cchuck_Off, s[3]),
                        }, s[0], "LCCHUCK");

            specs.Add(LCCHUCK);

            Console.WriteLine("\tAutomaton: {0}", LCCHUCK.ToString());
            Console.WriteLine("\tStates: {0}", LCCHUCK.Size);
            Console.WriteLine("\tTransitions: {0}", LCCHUCK.Transitions.Count());

            ////H24
            ///
           //CYCLATHE specs[18]
            var CYCLATHE = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_CYCLE_LATHE, s[1]),
                            new Transition(s[1], O_Lathe_Cycle_Start_On, s[2]),
                            new Transition(s[2], O_Lathe_Cycle_Start_Off, s[3]),
                            new Transition(s[3], Ver_I_Lathe_Cycle_Start_CYCLE_LATHE, s[4]),
                            new Transition(s[4], I_Lathe_Cycle_Start_Off_CYCLE_LATHE, s[5]),
                            new Transition(s[4], I_Lathe_Cycle_Start_On_CYCLE_LATHE, s[3]),
                            new Transition(s[5], Start_WAIT_CYCLE_END_LATHE, s[6]),
                            new Transition(s[6], End_WAIT_CYCLE_END_LATHE, s[7]),
                            new Transition(s[7], End_CYCLE_LATHE, s[0])
                        }, s[0], "CYCLATHE");

            specs.Add(CYCLATHE);

            Console.WriteLine("\tAutomaton: {0}", CYCLATHE.ToString());
            Console.WriteLine("\tStates: {0}", CYCLATHE.Size);
            Console.WriteLine("\tTransitions: {0}", CYCLATHE.Transitions.Count());

            //WCYCENDLAT specs[19]
            var WCYCENDLAT = new DeterministicFiniteAutomaton(new[]
            {
                            new Transition(s[0], Start_WAIT_CYCLE_END_LATHE, s[1]),
                            new Transition(s[1], Ver_I_Lathe_Cycle_Start_WAIT_CYCLE, s[2]),
                            new Transition(s[2], I_Lathe_Cycle_Start_Off_WAIT_CYCLE, s[1]),
                            new Transition(s[2], I_Lathe_Cycle_Start_On_WAIT_CYCLE, s[3]),
                            new Transition(s[3], Send_Endturn_to_CNC, s[4]),
                            new Transition(s[4], End_WAIT_CYCLE_END_LATHE, s[0])
                        }, s[0], "WCYCENDLAT");

            specs.Add(WCYCENDLAT);

            Console.WriteLine("\tAutomaton: {0}", WCYCENDLAT.ToString());
            Console.WriteLine("\tStates: {0}", WCYCENDLAT.Size);
            Console.WriteLine("\tTransitions: {0}", WCYCENDLAT.Transitions.Count());

            ////////////////////////////Interfaces


            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("Interfaces Serial Case:");

            //Interface I21 [0]
            var SI21 = new DeterministicFiniteAutomaton(new[]
           {
                            new Transition(s[0], Start_CLOSE_LATHE_DOOR, s[1]),
                            new Transition(s[1], End_CLOSE_LATHE_DOOR, s[0]),
                            new Transition(s[0], Start_JOB_LODOOR_GT022, s[2]),
                            new Transition(s[2], End_JOB_LODOOR_GT022, s[0]),
                            new Transition(s[0], Start_JOB_LODOOR_PT022, s[3]),
                            new Transition(s[3], End_JOB_LODOOR_PT022, s[0]),
                            new Transition(s[0], Start_JOB_OGRIP_GT022, s[4]),
                             new Transition(s[4], End_JOB_OGRIP_GT022, s[0]),
                             new Transition(s[0], Start_JOB_OGRIP_GT023, s[5]),
                             new Transition(s[5], End_JOB_OGRIP_GT023, s[0]),
                             new Transition(s[0], Start_JOB_OGRIP_PT022, s[6]),
                             new Transition(s[6], End_JOB_OGRIP_PT022, s[0]),
                             new Transition(s[0], Start_JOB_OGRIP_PT_GENER, s[7]),
                             new Transition(s[7], End_JOB_OGRIP_PT_GENER, s[0]),
                             new Transition(s[0], Start_JOB_CGRIP_GT022, s[8]),
                             new Transition(s[8], End_JOB_CGRIP_GT022, s[0]),
                             new Transition(s[0], Start_JOB_CGRIP_GT023, s[9]),
                             new Transition(s[9], End_JOB_CGRIP_GT023, s[0]),
                             new Transition(s[0], Start_JOB_LOCHUCK_GT022, s[10]),
                             new Transition(s[10], End_JOB_LOCHUCK_GT022, s[0]),
                             new Transition(s[0], Start_JOB_LOCHUCK_PT022, s[11]),
                             new Transition(s[11], End_JOB_LOCHUCK_PT022, s[0]),
                             new Transition(s[0], Start_JOB_LCCHUCK_PT022, s[12]),
                             new Transition(s[12], End_JOB_LCCHUCK_PT022, s[0]),
                             new Transition(s[0], Start_CYCLE_LATHE, s[13]),
                             new Transition(s[13], End_CYCLE_LATHE, s[0])

                        }, s[0], "SI21");

            interfaces.Add(SI21);

            Console.WriteLine("\tAutomaton: {0}", SI21.ToString());
            Console.WriteLine("\tStates: {0}", SI21.Size);
            Console.WriteLine("\tTransitions: {0}", SI21.Transitions.Count());

            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("Interfaces Parallel Case:");

            //Interface I21
            var PI21 = new DeterministicFiniteAutomaton(new[]
           {
                            new Transition(s[0], Start_CLOSE_LATHE_DOOR, s[1]),
                            new Transition(s[1], End_CLOSE_LATHE_DOOR, s[0]),
                            new Transition(s[0], Start_JOB_LODOOR_GT022, s[2]),
                            new Transition(s[2], End_JOB_LODOOR_GT022, s[0]),
                            new Transition(s[0], Start_JOB_LODOOR_PT022, s[3]),
                            new Transition(s[3], End_JOB_LODOOR_PT022, s[0]),
                        }, s[0], "PI21");//nome do automato

            interfacesP.Add(PI21);

            Console.WriteLine("\tAutomaton: {0}", PI21.ToString());
            Console.WriteLine("\tStates: {0}", PI21.Size);
            Console.WriteLine("\tTransitions: {0}", PI21.Transitions.Count());

            //Interface I22
            var PI22 = new DeterministicFiniteAutomaton(new[]
           {
                             new Transition(s[0], Start_JOB_OGRIP_GT022, s[1]),
                             new Transition(s[1], End_JOB_OGRIP_GT022, s[0]),
                             new Transition(s[0], Start_JOB_OGRIP_GT023, s[2]),
                             new Transition(s[2], End_JOB_OGRIP_GT023, s[0]),
                             new Transition(s[0], Start_JOB_OGRIP_PT022, s[3]),
                             new Transition(s[3], End_JOB_OGRIP_PT022, s[0]),
                             new Transition(s[0], Start_JOB_OGRIP_PT_GENER, s[4]),
                             new Transition(s[4], End_JOB_OGRIP_PT_GENER, s[0]),
                             new Transition(s[0], Start_JOB_CGRIP_GT022, s[5]),
                             new Transition(s[5], End_JOB_CGRIP_GT022, s[0]),
                             new Transition(s[0], Start_JOB_CGRIP_GT023, s[6]),
                             new Transition(s[6], End_JOB_CGRIP_GT023, s[0]),
                         }, s[0], "PI22");//nome do automato

            interfacesP.Add(PI22);

            Console.WriteLine("\tAutomaton: {0}", PI22.ToString());
            Console.WriteLine("\tStates: {0}", PI22.Size);
            Console.WriteLine("\tTransitions: {0}", PI22.Transitions.Count());

            //Interface I23
            var PI23 = new DeterministicFiniteAutomaton(new[]
           {
                             new Transition(s[0], Start_JOB_LOCHUCK_GT022, s[1]),
                             new Transition(s[1], End_JOB_LOCHUCK_GT022, s[0]),
                             new Transition(s[0], Start_JOB_LOCHUCK_PT022, s[2]),
                             new Transition(s[2], End_JOB_LOCHUCK_PT022, s[0]),
                             new Transition(s[0], Start_JOB_LCCHUCK_PT022, s[3]),
                             new Transition(s[3], End_JOB_LCCHUCK_PT022, s[0]),
                         }, s[0], "PI23");//nome do automato

            interfacesP.Add(PI23);

            Console.WriteLine("\tAutomaton: {0}", PI23.ToString());
            Console.WriteLine("\tStates: {0}", PI23.Size);
            Console.WriteLine("\tTransitions: {0}", PI23.Transitions.Count());

            //Interface I24
            var PI24 = new DeterministicFiniteAutomaton(new[]
           {
                             new Transition(s[0], Start_CYCLE_LATHE, s[1]),
                             new Transition(s[1], End_CYCLE_LATHE, s[0])
                         }, s[0], "PI24");//nome do automato

            interfacesP.Add(PI24);

            Console.WriteLine("\tAutomaton: {0}", PI24.ToString());
            Console.WriteLine("\tStates: {0}", PI24.Size);
            Console.WriteLine("\tTransitions: {0}", PI24.Transitions.Count());

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

            List<DeterministicFiniteAutomaton> plants, plants1, plants2, plants3, plants4, plants5, plants6, plants31, plants21, plants22, plants23, plants24, specs, especs1m, especs2m, specs1, specs2, specs3, specs4, specs5, specs6, specs21, specs22, specs23, specs24, specs31, specs2m, specsmono, interfaces1, interfaces2, sup1, sup2, plantaas, plantaas1, plantaas2;


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("------------------------HIERARQUICAL PROGRAM------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("------------------------------MECATRIME-----------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------");

            Hierarquical(out plants, out specs, out interfaces1, out interfaces2);
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("\tTotal Plants: {0}", plants.Count());
            Console.WriteLine("\tTotal Specifications: {0}", specs.Count());
            Console.WriteLine("\tTotal Interface Serial Case: {0}", interfaces1.Count());
            Console.WriteLine("\tTotal Interface Parallel Case: {0}", interfaces2.Count());

            var timer = new Stopwatch();
            string x;
            int h;
            x = "";
            while (x != "0")
            {
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("Choose one case:");
                Console.WriteLine("\tMonolithic Case = 1");
                Console.WriteLine("\tModular Case = 2");
                Console.WriteLine("\tSerial Case = 3");
                Console.WriteLine("\tParallel Case = 4");
                Console.WriteLine("\tExit = 0");
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine("Your Choice:");
                x = Console.ReadLine();
                while (x != "1" && x != "2" && x != "3" && x != "4" && x != "0")
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
                else if (x == "3")
                { h = 3; }
                else if (x == "4")
                { h = 4; }


                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (h == 1)
                {
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Case ----------------------------------------------------------");
                    Console.WriteLine("--------------------------------------------------------------------------");

                    var Specification = DeterministicFiniteAutomaton.ParallelComposition(specs);
                    //Console.WriteLine("Parallel Composition Specification OK");
                    //var Plant = DeterministicFiniteAutomaton.ParallelComposition(plants);
                    //Console.WriteLine("Parallel Composition Plants OK");
                    ////var K = Plant.ParallelCompositionWith(Specification);
                    //Console.WriteLine("Parallel Composition K OK");
                    //Console.WriteLine("Plant");
                    //Console.WriteLine("\tstates: {0}", Plant.Size);
                    //Console.WriteLine("\tTransitions: {0}", Plant.Transitions.Count());
                    Console.WriteLine("Specification");
                    Console.WriteLine("\tStates: {0}", Specification.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification.Transitions.Count());
                    //Console.WriteLine("K:");
                    //Console.WriteLine("\tStates: {0}", K.Size);
                    //Console.WriteLine("\tTransitions: {0}", K.Transitions.Count());
                    //// Controllability
                    //if (K.IsControllable(Plant))
                    //{
                    //    Console.WriteLine("\tK is controllable");
                    //}
                    //else
                    //{
                    //    Console.WriteLine("\tK is not controllable");
                    //}
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Supervisor :");                     
                    //timer.Restart();
                    var supmono = DeterministicFiniteAutomaton.MonolithicSupervisor(new[] { plants[0], plants[1], plants[2], plants[3], plants[4], plants[5], plants[6], plants[7], plants[8], plants[9], plants[10], plants[11], plants[12], plants[13], plants[14], plants[15], plants[16], plants[17], plants[18], plants[19], plants[20], plants[21], plants[22] }, specs, true);
                    //timer.Stop();
                    Console.WriteLine("\tStates: {0}", supmono.Size);
                    Console.WriteLine("\tTransitions: {0}", supmono.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (h == 2)
                {
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Modular Case ----------------------------------------------------------");
                    Console.WriteLine("--------------------------------------------------------------------------");
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    
                    Console.WriteLine("------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Modular Supervisor 1:");
                    plants1 = new[] { plants[0], plants[2], plants[3], plants[5], plants[6], plants[7], plants[8], plants[23], plants[26] }.ToList();
                    var Plant1 = DeterministicFiniteAutomaton.ParallelComposition(plants1);
                    specs1 = new[] { specs[0], specs[2], specs[4], specs[6], specs[7], specs[9], specs[10], specs[11] }.ToList();
                    var Specification1 = DeterministicFiniteAutomaton.ParallelComposition(specs1);
                    var K1 = Plant1.ParallelCompositionWith(Specification1);
                    Console.WriteLine("Plant1");
                    Console.WriteLine("\tstates: {0}", Plant1.Size);
                    Console.WriteLine("\tTransitions: {0}", Plant1.Transitions.Count());
                    Console.WriteLine("Specification1");
                    Console.WriteLine("\tStates: {0}", Specification1.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification1.Transitions.Count());
                    Console.WriteLine("K1:");
                    Console.WriteLine("\tStates: {0}", K1.Size);
                    Console.WriteLine("\tTransitions: {0}", K1.Transitions.Count());
                    //timer.Restart();
                    var supmod1 = DeterministicFiniteAutomaton.MonolithicSupervisor(new[] { plants[0], plants[2], plants[3], plants[5], plants[6], plants[7], plants[8], plants[23], plants[26] },
                       new[] { specs[0], specs[2], specs[4], specs[6], specs[7], specs[9], specs[10], specs[11] }, true);
                    //timer.Stop();
                    Console.WriteLine("supmod1");
                    Console.WriteLine("\tStates: {0}", supmod1.Size);
                    Console.WriteLine("\tTransitions: {0}", supmod1.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    ///////////////////////////////////////////////////////////////////////////////////////
                    Console.WriteLine("------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Modular Supervisor 2:");
                    plants2 = new[] { plants[1], plants[3], plants[4], plants[23], plants[24], plants[25] }.ToList();
                    var Plant2 = DeterministicFiniteAutomaton.ParallelComposition(plants2);
                    specs2 = new[] { specs[1], specs[3], specs[5], specs[8] }.ToList();
                    var Specification2 = DeterministicFiniteAutomaton.ParallelComposition(specs2);
                    var K2 = Plant2.ParallelCompositionWith(Specification2);
                    Console.WriteLine("Plant2");
                    Console.WriteLine("\tstates: {0}", Plant2.Size);
                    Console.WriteLine("\tTransitions: {0}", Plant2.Transitions.Count());
                    Console.WriteLine("Specification2");
                    Console.WriteLine("\tStates: {0}", Specification2.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification2.Transitions.Count());
                    Console.WriteLine("K2:");
                    Console.WriteLine("\tStates: {0}", K2.Size);
                    Console.WriteLine("\tTransitions: {0}", K2.Transitions.Count());
                    //timer.Restart();
                    var supmod2 = DeterministicFiniteAutomaton.MonolithicSupervisor(new[] { plants[1], plants[3], plants[4], plants[23], plants[24], plants[25] },
                        new[] { specs[1], specs[3], specs[5], specs[8] }, true);
                    //timer.Stop();
                    Console.WriteLine("supmod2");
                    Console.WriteLine("\tStates: {0}", supmod2.Size);
                    Console.WriteLine("\tTransitions: {0}", supmod2.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    Console.WriteLine("------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Modular Supervisor 3:");
                    plants3 = new[] { plants[9], plants[23], plants[11], plants[12] }.ToList();
                    var Plant3 = DeterministicFiniteAutomaton.ParallelComposition(plants3);
                    specs3 = new[] { specs[12], specs[13] }.ToList();
                    var Specification3 = DeterministicFiniteAutomaton.ParallelComposition(specs3);
                    var K3 = Plant3.ParallelCompositionWith(Specification3);
                    Console.WriteLine("Plant3");
                    Console.WriteLine("\tstates: {0}", Plant3.Size);
                    Console.WriteLine("\tTransitions: {0}", Plant3.Transitions.Count());
                    Console.WriteLine("Specification3");
                    Console.WriteLine("\tStates: {0}", Specification3.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification3.Transitions.Count());
                    Console.WriteLine("K3:");
                    Console.WriteLine("\tStates: {0}", K3.Size);
                    Console.WriteLine("\tTransitions: {0}", K3.Transitions.Count());
                    //timer.Restart();
                    var supmod3 = DeterministicFiniteAutomaton.MonolithicSupervisor(new[] { plants[9], plants[23], plants[11], plants[12] },
                        new[] { specs[12], specs[13] }, true);
                    //timer.Stop();
                    Console.WriteLine("supmod3");
                    Console.WriteLine("\tStates: {0}", supmod3.Size);
                    Console.WriteLine("\tTransitions: {0}", supmod3.Transitions.Count());
                    // Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    Console.WriteLine("------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Modular Supervisor 4:");
                    plants4 = new[] { plants[14], plants[24] }.ToList();
                    var Plant4 = DeterministicFiniteAutomaton.ParallelComposition(plants4);
                    specs4 = new[] { specs[14], specs[15] }.ToList();
                    var Specification4 = DeterministicFiniteAutomaton.ParallelComposition(specs4);
                    var K4 = Plant4.ParallelCompositionWith(Specification4);
                    Console.WriteLine("Plant4");
                    Console.WriteLine("\tstates: {0}", Plant4.Size);
                    Console.WriteLine("\tTransitions: {0}", Plant4.Transitions.Count());
                    Console.WriteLine("Specification4");
                    Console.WriteLine("\tStates: {0}", Specification4.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification4.Transitions.Count());
                    Console.WriteLine("K4:");
                    Console.WriteLine("\tStates: {0}", K4.Size);
                    Console.WriteLine("\tTransitions: {0}", K4.Transitions.Count());
                    //timer.Restart();
                    var supmod4 = DeterministicFiniteAutomaton.MonolithicSupervisor(new[] { plants[14], plants[24] },
                        new[] { specs[14], specs[15] }, true);
                    //timer.Stop();
                    Console.WriteLine("supmod4");
                    Console.WriteLine("\tStates: {0}", supmod4.Size);
                    Console.WriteLine("\tTransitions: {0}", supmod4.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);
                    
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    Console.WriteLine("------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Modular Supervisor 5:");
                    plants5 = new[] { plants[18], plants[16], plants[25], plants[17] }.ToList();
                    var Plant5 = DeterministicFiniteAutomaton.ParallelComposition(plants5);
                    specs5 = new[] { specs[16], specs[17] }.ToList();
                    var Specification5 = DeterministicFiniteAutomaton.ParallelComposition(specs5);
                    var K5 = Plant5.ParallelCompositionWith(Specification5);
                    Console.WriteLine("Plant5");
                    Console.WriteLine("\tstates: {0}", Plant5.Size);
                    Console.WriteLine("\tTransitions: {0}", Plant5.Transitions.Count());
                    Console.WriteLine("Specification5");
                    Console.WriteLine("\tStates: {0}", Specification5.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification5.Transitions.Count());
                    Console.WriteLine("K5:");
                    Console.WriteLine("\tStates: {0}", K5.Size);
                    Console.WriteLine("\tTransitions: {0}", K5.Transitions.Count());
                    //timer.Restart();
                    var supmod5 = DeterministicFiniteAutomaton.MonolithicSupervisor(new[] { plants[16], plants[25], plants[17], plants[18] },
                        new[] { specs[16], specs[17] }, true);
                    //timer.Stop();
                    Console.WriteLine("supmod5");
                    Console.WriteLine("\tStates: {0}", supmod5.Size);
                    Console.WriteLine("\tTransitions: {0}", supmod5.Transitions.Count());
                    // Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    Console.WriteLine("------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Modular Supervisor 6:");
                    plants6 = new[] { plants[19], plants[26], plants[21], plants[22] }.ToList();
                    var Plant6 = DeterministicFiniteAutomaton.ParallelComposition(plants6);
                    specs6 = new[] { specs[18], specs[19] }.ToList();
                    var Specification6 = DeterministicFiniteAutomaton.ParallelComposition(specs6);
                    var K6 = Plant6.ParallelCompositionWith(Specification6);
                    Console.WriteLine("Plant6");
                    Console.WriteLine("\tstates: {0}", Plant6.Size);
                    Console.WriteLine("\tTransitions: {0}", Plant6.Transitions.Count());
                    Console.WriteLine("Specification6");
                    Console.WriteLine("\tStates: {0}", Specification6.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification6.Transitions.Count());
                    Console.WriteLine("K6:");
                    Console.WriteLine("\tStates: {0}", K6.Size);
                    Console.WriteLine("\tTransitions: {0}", K6.Transitions.Count());
                    //timer.Restart();
                    var supmod6 = DeterministicFiniteAutomaton.MonolithicSupervisor(new[] { plants[19], plants[26], plants[21], plants[22] },
                        new[] { specs[18], specs[19] }, true);
                    //timer.Stop();
                    Console.WriteLine("supmod6");
                    Console.WriteLine("\tStates: {0}", supmod6.Size);
                    Console.WriteLine("\tTransitions: {0}", supmod6.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);
                   

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    Console.WriteLine("------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Modularity test");
                    //timer.Restart();
                    var SModular = supmod1.ParallelCompositionWith(supmod2, supmod4, supmod5, supmod3, supmod6);
                    Console.WriteLine("SModular:");
                    Console.WriteLine("\tStates: {0}", SModular.Size);
                    Console.WriteLine("\tTransitions: {0}", SModular.Transitions.Count());
                    var smodulartrim = SModular.Trim;
                    Console.WriteLine("SModulartrim:");
                    Console.WriteLine("\tStates: {0}", smodulartrim.Size);
                    Console.WriteLine("\tTransitions: {0}", smodulartrim.Transitions.Count());
                    //timer.Stop();
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    ///////////////////////////////////////////////////////////////////////////////////////////
                    //timer.Restart();
                    //var supmono = DeterministicFiniteAutomaton.MonolithicSupervisor(new[] { plants[0], plants[1], plants[2], plants[3], plants[4], plants[5], plants[6], plants[7], plants[8], plants[9], plants[23], plants[11], plants[12], plants[24], plants[14], plants[25], plants[16], plants[17], plants[18], plants[19], plants[26], plants[21], plants[22] }, specs, true);
                    //var supmonotrim = supmono.Trim;
                    //Console.WriteLine("supmonotrim:");
                    //Console.WriteLine("\tStates: {0}", supmonotrim.Size);
                    //Console.WriteLine("\tTransitions: {0}", supmonotrim.Transitions.Count());
                    //isomorf(supmono, SModular);
                    //timer.Stop();
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (h == 3)
                {
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("-------------------------------Serial Case--------------------------------");
                    Console.WriteLine("--------------------------------------------------------------------------");
                    
                    ////declarando variaveis
                   
                    ////H11
                    Console.WriteLine("High Level (H11)");
                    plants1 = new[] { plants[0], plants[1], plants[2], plants[3], plants[4], plants[5], plants[6], plants[7], plants[8] }.ToList(); 
                    Console.WriteLine("\tTotal Plants : {0}", plants1.Count());
                    specs1 = new[] { specs[0], specs[1], specs[2], specs[3], specs[4], specs[5], specs[6], specs[7], specs[8], specs[9], specs[10], specs[11] }.ToList();
                    Console.WriteLine("\tTotal Specifications : {0}", specs1.Count());
                   
                    //H21
                    Console.WriteLine("Low Level (H21)");
                    plants21 = new[] { plants[9], plants[10], plants[11], plants[12], plants[14], plants[13], plants[18], plants[16], plants[15], plants[17], plants[19], plants[20], plants[21], plants[22] }.ToList();
                    Console.WriteLine("\tTotal Plants : {0}", plants21.Count());
                    specs21 = new[] { specs[12], specs[13], specs[14], specs[15], specs[16], specs[17], specs[18], specs[19] }.ToList();
                    Console.WriteLine("\tTotal Specifications: {0}", specs21.Count());

                    //I21
                    Console.WriteLine("Interfaces");
                    Console.WriteLine("\tTotal: {0}", interfaces1.Count());
                    Console.WriteLine("--------------------------------------------------------------------------");

                    //// Composições                

                    Console.WriteLine("High Level Composition (H11):");
                    var Specification1 = DeterministicFiniteAutomaton.ParallelComposition(specs1);
                    // Console.WriteLine("Parallel Composition Specification 1 OK");
                    var Plant1 = DeterministicFiniteAutomaton.ParallelComposition(plants1);
                    //Console.WriteLine("Parallel Composition Plants 1 OK");
                    var interface1 = DeterministicFiniteAutomaton.ParallelComposition(interfaces1);
                    //Console.WriteLine("Parallel Composition Interfaces Connected with High Level OK");
                    var Plantint1 = Plant1.ParallelCompositionWith(interface1);
                    //Console.WriteLine("Parallel Composition Plantint1 OK");
                    var K11 = Plant1.ParallelCompositionWith(Specification1);
                    var K1 = K11.ParallelCompositionWith(interface1);
                    //Console.WriteLine("Parallel Composition K1 OK");                  
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
                    //Console.WriteLine("Ktrim:");
                    //Console.WriteLine("\tStates: {0}", K1.Trim.Size);
                    //Console.WriteLine("\tTransitions: {0}", K1.Trim.Transitions.Count());

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Low Level Composition (H21):");
                    var Specification21 = DeterministicFiniteAutomaton.ParallelComposition(specs21);
                    //Console.WriteLine("Parallel Composition Specification21 OK");
                    var Plant21 = DeterministicFiniteAutomaton.ParallelComposition(plants21);
                    //Console.WriteLine("Parallel Composition Plants21 OK");
                    var interface21 = interfaces1[0];
                    var K211 = Plant21.ParallelCompositionWith(Specification21);
                    var K21 = K211.ParallelCompositionWith(interface21);
                    //Console.WriteLine("Parallel Composition K21 OK");
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
                    //Console.WriteLine("\tStates: {0}", K21.Trim.Size);
                    //Console.WriteLine("\tTransitions: {0}", K21.Trim.Transitions.Count());

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ////Sigma_A and Sigma_R////////////////////////////////////////////////////////////////////
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                    var eventsinterface21 = interface21.Events.ToList();//SigmaG_I  

                    //Find Events in Interface
                    //for (int c = 0; c < eventsinterface21.Count(); c++)
                    // {
                    //     Console.WriteLine("\teventos interface 21: {0}", eventsinterface21[c]);
                    // }
                    //Console.WriteLine("\teventos interface: {0}", eventsinterface21);

                    var requestevents21 = new[] { eventsinterface21[0], eventsinterface21[2], eventsinterface21[4], eventsinterface21[6], eventsinterface21[8], eventsinterface21[10], eventsinterface21[12], eventsinterface21[14], eventsinterface21[16], eventsinterface21[18], eventsinterface21[20], eventsinterface21[22], eventsinterface21[24] }.ToList();//SigmaR
                    var answerevents21 = new[] { eventsinterface21[1], eventsinterface21[3], eventsinterface21[5], eventsinterface21[7], eventsinterface21[9], eventsinterface21[11], eventsinterface21[13], eventsinterface21[15], eventsinterface21[17], eventsinterface21[19], eventsinterface21[21], eventsinterface21[23], eventsinterface21[25] }.ToList();//SigmaA


                    //Supervisors Compositions///////////////////////////////////////////////////////////////////////

                    ////High Level Composition/////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Supervisor High Level (H11):");
                    plants1.Add(interface1);
                    //timer.Restart();
                    var supH11 = DeterministicFiniteAutomaton.MonolithicSupervisor(plants1, specs1, true);
                    ////var supH11 = DeterministicFiniteAutomaton.MonolithicSupervisor(new[] { plants[0], plants[1], plants[2], plants[3], plants[4], plants[5], plants[6], plants[7], plants[8], interfaces1[0] },
                    ////   new[] { specs[0], specs[1], specs[2], specs[3], specs[4], specs[5], specs[6], specs[7], specs[8], specs[9], specs[10], specs[11] }, true);
                    //timer.Stop();
                    Console.WriteLine("\tStates: {0}", supH11.Size);
                    Console.WriteLine("\tTransitions: {0}", supH11.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    //Low Level Composition/////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Supervisor Low Level (H21):");
                    specs21.Add(interface21);
                    //timer.Restart();
                    var supH21 = DeterministicFiniteAutomaton.MonolithicSupervisor(plants21, specs21, true);
                    ////var supH21 = DeterministicFiniteAutomaton.MonolithicSupervisor(new[] { plants[9], plants[10], plants[11], plants[12], plants[14], plants[13], plants[18], plants[16], plants[15], plants[17], plants[19], plants[20], plants[21], plants[22]},
                    ////new[] { specs[12], specs[13], specs[14], specs[15], specs[16], specs[17], specs[18], specs[19], interfaces1[0] }, true);
                    //timer.Stop();
                    Console.WriteLine("\tStates: {0}", supH21.Size);
                    Console.WriteLine("\tTransitions: {0}", supH21.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    //Flat Supervisor////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("\tSupervisors Composition:");
                    var s11 = supH11.ParallelCompositionWith(supH21, interface21);
                    Console.WriteLine("\tStates: {0}", s11.Size);
                    Console.WriteLine("\tTransitions: {0}", s11.Trim.Transitions.Count());

                    //Monolithic Supervisor//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("\tMonolithic Supervisor:");
                    var smono = DeterministicFiniteAutomaton.MonolithicSupervisor(new[] { plants[0], plants[1], plants[2], plants[3], plants[4], plants[5], plants[6], plants[7], plants[8], plants[9], plants[10], plants[11], plants[12], plants[13], plants[14], plants[15], plants[16], plants[17], plants[18], plants[19], plants[20], plants[21], plants[22] }, specs, true);
                    Console.WriteLine("\tStates: {0}", smono.Size);
                    Console.WriteLine("\tTransitions: {0}", smono.Transitions.Count());

                    //Isomorfism//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("test isomorfism");
                    isomorf(s11, smono);

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

                    //////Synthesis////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //Console.WriteLine("--------------------------------------------------------------------------");
                    //Console.WriteLine("Synthesis Program:");
                    //Console.WriteLine("--------------------------------------------------------------------------");

                    //var answereventslist = new[] { eventsinterface21[1], eventsinterface21[3], eventsinterface21[5], eventsinterface21[7], eventsinterface21[9], eventsinterface21[11], eventsinterface21[13], eventsinterface21[15], eventsinterface21[17], eventsinterface21[19], eventsinterface21[21], eventsinterface21[23], eventsinterface21[25] }.ToList();// list with all response events from all interfaces connected inferiorly in sequence

                    //var numberanswer = new[] { 13 }.ToList();//Number of response events from each interface connected inferiorly

                    //Console.WriteLine("Synthesis H11");
                    //sintesesupHHISC(Plant1, Specification1, interfaces1, answereventslist, numberanswer, out var supH11);
                    //Console.WriteLine("\tStates: {0}", supH11.Size);
                    //Console.WriteLine("\tTransitions: {0}", supH11.Transitions.Count());

                    //Console.WriteLine("--------------------------------------------------------------------------");

                    //Console.WriteLine("Synthesis H21");
                    //sintesesupLHISC(Plant21, Specification21, interface21, requestevents21, answerevents21, out var supH21);
                    //Console.WriteLine("\tStates: {0}", supH21.Size);
                    //Console.WriteLine("\tTransitions: {0}", supH21.Transitions.Count());

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    interface1 = null;
                    interface21 = null;
                    Specification1 = null;
                    Specification21 = null;
                    Plant1 = null;
                    Plant21 = null;
                    K11 = null;
                    K1 = null;
                    K211 = null;
                    K21 = null;
                    supH11 = null;
                    supH21 = null;
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (h == 4)
                {
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("-------------------------------Parallel Case------------------------------");
                    Console.WriteLine("--------------------------------------------------------------------------");
                    
                    //Declaretion of Variables

                    ////H11
                    Console.WriteLine("High Level (H11)");
                    plants1 = new[] { plants[0], plants[1], plants[2], plants[3], plants[4], plants[5], plants[6], plants[7], plants[8] }.ToList(); // , plants[8]
                    Console.WriteLine("\tTotal Plants : {0}", plants1.Count());
                    specs1 = new[] { specs[0], specs[1], specs[2], specs[3], specs[4], specs[5], specs[6], specs[7], specs[8], specs[9], specs[10], specs[11] }.ToList();//, specs[5], specs[6], specs[7], specs[8], specs[9], specs[10]
                    Console.WriteLine("\tTotal Specifications : {0}", specs1.Count());
                    
                    //H21
                    Console.WriteLine("Low Level (H21)");
                    plants21 = new[] { plants[9], plants[10], plants[11], plants[12] }.ToList();
                    Console.WriteLine("\tTotal Plants : {0}", plants21.Count());
                    specs21 = new[] { specs[12], specs[13] }.ToList();
                    Console.WriteLine("\tTotal Specifications: {0}", specs21.Count());

                    //H22
                    Console.WriteLine("Low Level (H22)");
                    plants22 = new[] { plants[14], plants[13] }.ToList();
                    Console.WriteLine("\tTotal Plants : {0}", plants22.Count());
                    specs22 = new[] { specs[14], specs[15] }.ToList();
                    Console.WriteLine("\tTotal Specifications: {0}", specs22.Count());

                    //H23
                    Console.WriteLine("Low Level (H23)");
                    plants23 = new[] { plants[18], plants[16], plants[15], plants[17] }.ToList();
                    Console.WriteLine("\tTotal Plants : {0}", plants23.Count());
                    specs23 = new[] { specs[16], specs[17] }.ToList();
                    Console.WriteLine("\tTotal Specifications: {0}", specs23.Count());

                    //H24
                    Console.WriteLine("Low Level (H24)");
                    plants24 = new[] { plants[19], plants[20], plants[21], plants[22] }.ToList();
                    Console.WriteLine("\tTotal Plants : {0}", plants24.Count());
                    specs24 = new[] { specs[18], specs[19] }.ToList();
                    Console.WriteLine("\tTotal Specifications: {0}", specs24.Count());

                    //interfaces (I21, I22, I23 and I24)
                    Console.WriteLine("Interfaces");
                    Console.WriteLine("\tTotal: {0}", interfaces2.Count());
                    Console.WriteLine("--------------------------------------------------------------------------");

                    //// Compositions                

                    Console.WriteLine("High Level Composition (H11):");
                    var Specification1 = DeterministicFiniteAutomaton.ParallelComposition(specs1);
                    //Console.WriteLine("Parallel Composition Specification1 OK");
                    var Plant1 = DeterministicFiniteAutomaton.ParallelComposition(plants1);
                    //Console.WriteLine("Parallel Composition Plants1 OK");
                    var interface1 = DeterministicFiniteAutomaton.ParallelComposition(interfaces2);
                    //Console.WriteLine("Parallel Composition Interfaces Connected with High Level OK");                    
                    var K11 = Plant1.ParallelCompositionWith(Specification1);
                    var K1 = K11.ParallelCompositionWith(interface1);
                    Console.WriteLine("Parallel Composition K1 OK");
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
                    

                    //Attention!!!:This composition consumes a lot of processing, so it was commented.
                    //However, if you activate it, remember that it will take hours.

                        //var Plantint1 = Plant1.ParallelCompositionWith(interface1);
                        //Console.WriteLine("Parallel Composition Plantint1 OK");
                        //if (K1.IsControllable(Plantint1))
                        //{
                        //    Console.WriteLine("\tK is controllable");
                        //}
                        //else
                        //{
                        //    Console.WriteLine("\tK is not controllable");
                        //}

                    //Console.WriteLine("Ktrim:");
                    //Console.WriteLine("\tStates: {0}", K1.Trim.Size);
                    //Console.WriteLine("\tTransitions: {0}", K1.Trim.Transitions.Count());

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Low Level Composition (H21):");
                    var Specification21 = DeterministicFiniteAutomaton.ParallelComposition(specs21);
                    //Console.WriteLine("Parallel Composition Specification21 OK");
                    var Plant21 = DeterministicFiniteAutomaton.ParallelComposition(plants21);
                    //Console.WriteLine("Parallel Composition Plants21 OK");
                    var interface21 = interfaces2[0];
                    //Console.WriteLine("Parallel Composition interface21 OK");
                    var K211 = Plant21.ParallelCompositionWith(Specification21);
                    var K21 = K211.ParallelCompositionWith(interface21);
                    //Console.WriteLine("Parallel Composition K21 OK");
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
                    //Console.WriteLine("\tStates: {0}", K21.Trim.Size);
                    //Console.WriteLine("\tTransitions: {0}", K21.Trim.Transitions.Count());

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Low Level Composition (H22):");
                    var Specification22 = DeterministicFiniteAutomaton.ParallelComposition(specs22);
                    //Console.WriteLine("Parallel Composition Specification22 OK");
                    var Plant22 = DeterministicFiniteAutomaton.ParallelComposition(plants22);
                    //Console.WriteLine("Parallel Composition Plants22 OK");
                    var interface22 = interfaces2[1];
                    //Console.WriteLine("Parallel Composition interface22 OK");
                    var K221 = Plant22.ParallelCompositionWith(Specification22);
                    var K22 = K221.ParallelCompositionWith(interface22);
                    //Console.WriteLine("Parallel Composition K22 OK");
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
                    //Console.WriteLine("\tStates: {0}", K22.Trim.Size);
                    //Console.WriteLine("\tTransitions: {0}", K22.Trim.Transitions.Count());

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Low Level Composition (H23):");
                    var Specification23 = DeterministicFiniteAutomaton.ParallelComposition(specs23);
                    //Console.WriteLine("Parallel Composition Specification23 OK");
                    var Plant23 = DeterministicFiniteAutomaton.ParallelComposition(plants23);
                    //Console.WriteLine("Parallel Composition Plants23 OK");
                    var interface23 = interfaces2[2];
                    //Console.WriteLine("Parallel Composition interface23 OK");
                    var K231 = Plant23.ParallelCompositionWith(Specification23);
                    var K23 = K231.ParallelCompositionWith(interface23);
                    //Console.WriteLine("Parallel Composition K23 OK");
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
                    //Console.WriteLine("\tStates: {0}", K23.Trim.Size);
                    //Console.WriteLine("\tTransitions: {0}", K23.Trim.Transitions.Count());
                    
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Low Level Composition (H24):");
                    var Specification24 = DeterministicFiniteAutomaton.ParallelComposition(specs24);
                    //Console.WriteLine("Parallel Composition Specification24 OK");
                    var Plant24 = DeterministicFiniteAutomaton.ParallelComposition(plants24);
                    //Console.WriteLine("Parallel Composition Plants24 OK");
                    var interface24 = interfaces2[3];
                    //Console.WriteLine("Parallel Composition interface24 OK");
                    var K241 = Plant24.ParallelCompositionWith(Specification24);
                    var K24 = K241.ParallelCompositionWith(interface24);
                    //Console.WriteLine("Parallel Composition K24 OK");
                    Console.WriteLine("Plant");
                    Console.WriteLine("\tStates: {0}", Plant24.Size);
                    Console.WriteLine("\tTransitions: {0}", Plant24.Transitions.Count());
                    Console.WriteLine("Specification");
                    Console.WriteLine("\tStates: {0}", Specification24.Size);
                    Console.WriteLine("\tTransitions: {0}", Specification24.Transitions.Count());
                    Console.WriteLine("Interfaces");
                    Console.WriteLine("\tStates: {0}", interface24.Size);
                    Console.WriteLine("\tTransitions: {0}", interface24.Transitions.Count());
                    Console.WriteLine("K:");
                    Console.WriteLine("\tStates: {0}", K24.Size);
                    Console.WriteLine("\tTransitions: {0}", K24.Transitions.Count());
                    if (K24.IsControllable(Plant24))
                    {
                        Console.WriteLine("\tK is controllable");
                    }
                    else
                    {
                        Console.WriteLine("\tK is not controllable");
                    }
                    //Console.WriteLine("Ktrim:");
                    //Console.WriteLine("\tStates: {0}", K24.Trim.Size);
                    //Console.WriteLine("\tTransitions: {0}", K24.Trim.Transitions.Count());

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
                    //Console.WriteLine("\teventos interface: {0}", eventsinterface23);

                    var eventsinterface24 = interface24.Events.ToList();//SigmaG_I24  

                    //Find Events in Interface I24
                    //for (int c = 0; c < eventsinterface24.Count(); c++)
                    //{
                    //    Console.WriteLine("\teventos interface 23: {0}", eventsinterface24[c]);
                    //}
                    //Console.WriteLine("\teventos interface: {0}", eventsinterface24);

                    var requestevents21 = new[] { eventsinterface21[0], eventsinterface21[2], eventsinterface21[4] }.ToList();//SigmaR
                    var answerevents21 = new[] { eventsinterface21[1], eventsinterface21[3], eventsinterface21[5] }.ToList();//SigmaA

                    var requestevents22 = new[] { eventsinterface22[0], eventsinterface22[2], eventsinterface22[4], eventsinterface22[6], eventsinterface22[8], eventsinterface22[10] }.ToList();//SigmaR
                    var answerevents22 = new[] { eventsinterface22[1], eventsinterface22[3], eventsinterface22[5], eventsinterface22[7], eventsinterface22[9], eventsinterface22[11] }.ToList();//SigmaA

                    var requestevents23 = new[] { eventsinterface23[0], eventsinterface23[2], eventsinterface23[4] }.ToList();//SigmaR
                    var answerevents23 = new[] { eventsinterface23[1], eventsinterface23[3], eventsinterface23[5] }.ToList();//SigmaA

                    var requestevents24 = new[] { eventsinterface24[0] }.ToList();//SigmaR
                    var answerevents24 = new[] { eventsinterface24[1] }.ToList();//SigmaA

                    ////Supervisors Compositions///////////////////////////////////////////////////////////////////////
                    //High Level Composition/////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Supervisor High Level (H11):");
                    plants1.Add(interface1);
                    //timer.Restart();
                    //acha o supervisor monolitico
                    var supH11 = DeterministicFiniteAutomaton.MonolithicSupervisor(plants1, specs1, true);
                    //timer.Stop();
                    Console.WriteLine("\tStates: {0}", supH11.Size);
                    Console.WriteLine("\tTransitions: {0}", supH11.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    //Low Level Composition/////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Supervisor Low Level (H21):");
                    specs21.Add(interface21);
                    //timer.Restart();
                    var supH21 = DeterministicFiniteAutomaton.MonolithicSupervisor(plants21, specs21, true);
                    //timer.Stop();
                    Console.WriteLine("\tStates: {0}", supH21.Size);
                    Console.WriteLine("\tTransitions: {0}", supH21.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Supervisor Low Level (H23):");
                    specs23.Add(interface23);
                    //timer.Restart();
                    var supH23 = DeterministicFiniteAutomaton.MonolithicSupervisor(plants23, specs23, true);
                    //timer.Stop();
                    Console.WriteLine("\tStates: {0}", supH23.Size);
                    Console.WriteLine("\tTransitions: {0}", supH23.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Monolithic Supervisor Low Level (H24):");
                    specs24.Add(interface24);
                    //timer.Restart();
                    var supH24 = DeterministicFiniteAutomaton.MonolithicSupervisor(plants24, specs24, true);
                    //timer.Stop();
                    Console.WriteLine("\tStates: {0}", supH24.Size);
                    Console.WriteLine("\tTransitions: {0}", supH24.Transitions.Count());
                    //Console.WriteLine("\tComputation Time for Sup: {0}", timer.ElapsedMilliseconds / 1000.0);

                    //Flat Supervisor////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("\tSupervisors Composition:");
                    var s11 = supH11.ParallelCompositionWith(supH22, supH23, supH21, supH24, interface21, interface22, interface23, interface24);
                    Console.WriteLine("\tStates: {0}", s11.Size);
                    Console.WriteLine("\tTransitions: {0}", s11.Transitions.Count());

                    //Monolithic Supervisor//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //if using the HISC synthesis program disable this section
                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("\tMonolithic Supervisor:");
                    var smono = DeterministicFiniteAutomaton.MonolithicSupervisor(new[] { plants[0], plants[1], plants[2], plants[3], plants[4], plants[5], plants[6], plants[7], plants[8], plants[9], plants[10], plants[11], plants[12], plants[13], plants[14], plants[15], plants[16], plants[17], plants[18], plants[19], plants[20], plants[21], plants[22] }
                    , specs, true);
                    Console.WriteLine("\tStates: {0}", smono.Size);
                    Console.WriteLine("\tTransitions: {0}", smono.Transitions.Count());


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
                    var supplan11 = supH11.ParallelCompositionWith(Plant1);
                    var supplan21 = supH21.ParallelCompositionWith(Plant21);

                    Verificationpoint(supplan11, supplan21, interface21, requestevents21, answerevents21);

                    Console.WriteLine("--------------------------------------------------------------------------");

                    Console.WriteLine("Relationship between H11 and H22");

                    var supplan22 = supH22.ParallelCompositionWith(Plant22);
                    Verificationpoint(supplan11, supplan22, interface22, requestevents22, answerevents22);

                    Console.WriteLine("--------------------------------------------------------------------------");

                    Console.WriteLine("Relationship between H11 and H23");
                    var supplan23 = supH23.ParallelCompositionWith(Plant23);
                    Verificationpoint(supplan11, supplan23, interface23, requestevents23, answerevents23);

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("Relationship between H11 and H24");
                    var supplan24 = supH24.ParallelCompositionWith(Plant24);
                    Verificationpoint(supplan11, supplan24, interface24, requestevents24, answerevents24);

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine("--------------------------------------------------------------------------");


                    ////Synthesis////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    //Console.WriteLine("--------------------------------------------------------------------------");
                    //Console.WriteLine("Synthesis Program:");
                    //Console.WriteLine("--------------------------------------------------------------------------");

                    //var answereventslist = new[] { eventsinterface21[1], eventsinterface21[3], eventsinterface21[5], eventsinterface22[1], eventsinterface22[3], eventsinterface22[5], eventsinterface22[7], eventsinterface22[9], eventsinterface22[11], eventsinterface23[1], eventsinterface23[3], eventsinterface23[5], eventsinterface24[1]}.ToList();// list with all response events from all interfaces connected inferiorly in sequence

                    //var numberanswer = new[] { 3, 6, 3, 1}.ToList();//Number of response events from each interface connected inferiorly

                    //Console.WriteLine("--------------------------------------------------------------------------");

                    ////Attention!!!:This composition consumes a lot of processing, so it was commented.
                    ////However, if you activate it, remember that it will take hours.
                    //    //Console.WriteLine("Synthesis H11");
                    //    //sintesesupHHISC(Plant1, Specification1, interfaces2, answereventslist, numberanswer, out var supH11);
                    //    //Console.WriteLine("\tStates: {0}", supH11.Size);
                    //    //Console.WriteLine("\tTransitions: {0}", supH11.Transitions.Count());

                    //Console.WriteLine("--------------------------------------------------------------------------");

                    //Console.WriteLine("Synthesis H21");
                    //sintesesupLHISC(Plant21, Specification21, interface21, requestevents21, answerevents21, out var supH21);
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

                    //Console.WriteLine("Synthesis H24");
                    //sintesesupLHISC(Plant24, Specification24, interface24, requestevents24, answerevents24, out var supH24);
                    //Console.WriteLine("\tStates: {0}", supH24.Size);
                    //Console.WriteLine("\tTransitions: {0}", supH24.Transitions.Count());

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    interface1 = null;
                    interface21 = null;
                    interface22 = null;
                    interface23 = null;
                    interface24 = null;
                    Specification1 = null;
                    Specification21 = null;
                    Specification22 = null;
                    Specification23 = null;
                    Specification24 = null;
                    Plant1 = null;
                    Plant21 = null;
                    Plant22 = null;
                    Plant23 = null;
                    Plant24 = null;
                    K11 = null;
                    K1 = null;
                    K211 = null;
                    K21 = null;
                    K221 = null;
                    K22 = null;
                    K231 = null;
                    K23 = null;
                    K241 = null;
                    K24 = null;
                    supH11 = null;
                    supH21 = null;
                    supH22 = null;
                    supH23 = null;
                    supH24 = null;
                }

                Console.WriteLine("--------------------------------------------------------------------------");
            }
            //Console.ReadLine();

        }
    }
}

