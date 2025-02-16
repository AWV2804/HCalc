﻿namespace HexCalc
{
    public enum Input
    {
        NUM_INPUT_BOX_0,
        BIT_VISUALIZER_0 = NUM_INPUT_BOX_0,
        NUM_INPUT_BOX_1,
        BIT_VISUALIZER_1 = NUM_INPUT_BOX_1,
        NUM_INPUT_BOX_R,
        BIT_VISUALIZER_R = NUM_INPUT_BOX_R,
        NUM_INPUT_BOX_CNT,
        BIT_VISUALIZER_CNT = NUM_INPUT_BOX_CNT,
        NUM_INPUT_BOX_INV,
        BIT_VISUALIZER_INV = NUM_INPUT_BOX_INV,
    }

    public enum Instruction_Format
    {
        R_TYPE,
        I_TYPE,
        S_TYPE,
        SB_TYPE,
        U_TYPE,
        UJ_TYPE
    }

    public static class Values
    {
        public static string hexValue { get; set; } = "";
        public static string decValue { get; set; } = "";
        public static string binValue { get; set; } = "";
    }
}