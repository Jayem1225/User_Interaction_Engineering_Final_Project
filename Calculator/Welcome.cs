using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Welcome : Form
    {
        //string input;
        //string output = "0";
        //double result;
        bool radians = true;
        bool init = false;

        public Welcome()
        {
            InitializeComponent();
        }

        private int FindCloseParenIndex(string source, int start_index) //returns -1 if no matching close is found
        {
            int open_paren = 1;
            int close_paren_index = -1;
            for (int i=start_index; i<source.Length; ++i)
            {
                if (source[i].Equals("("))
                    ++open_paren;
                if (source[i].Equals(")"))
                {
                    --open_paren;
                    if (open_paren == 0)
                        return i;
                }
            }

            return close_paren_index;
        }

        private string OrderOfOperations(string source)
        {

            //Statistics
            while (source.IndexOf("sum(") > -1)
            {
                int start_index = source.IndexOf("sum(");
                int close_index = FindCloseParenIndex(source, start_index + start_index + 4);
                if (close_index < 0)
                    return "ERROR";

            }

            /*    //Calculate Parenthesis
                for (int i = 0; i < source.Length - 1; ++i)
                {
                    if (source_array[i] == '(')
                        if (source_array[i + 1] == ')')
                        {
                            source = source.Substring(0, i) + source.Substring(i + 2);
                            --i;
                        }
                        else
                            for (int j = i + 1; j < source.Length; ++j)
                            {
                                int paren_count = 1;
                                if (source_array[j] == '(')
                                    ++paren_count;
                                else if (source_array[j] == ')')
                                {
                                    --paren_count;
                                    if (paren_count < 1)
                                    {
                                        if (j == source.Length - 1)
                                            source = source.Substring(0, i) + OrderOfOperations(source.Substring(i));
                                        else
                                            source = source.Substring(0, i) + OrderOfOperations(source.Substring(i + 1, j - i)) + source.Substring(j + 1);
                                        --i;
                                        break;
                                    }
                                }

                            }
                } */

            if (source.IndexOf("avg(") > -1)
            {
                int sum_index = source.IndexOf("avg(") + 4;

                if (sum_index >= source.Length)
                    return "ERROR";

                int num_values = 1;
                for (int i = sum_index; i < source.Length && source[i] != ')'; ++i)
                {
                    if (source[i] == ',')
                        ++num_values;
                }

                double[] data = new double[num_values];
                string value = "";
                for (int i = sum_index; i < source.Length && source[i] != ')'; ++i)
                {
                    if (source[i] == ',')
                    {
                        data[num_values - 1] = double.Parse(value);
                        value = "";
                        --num_values;
                    }
                    else
                        value += source[i];
                    if  (i + 1 >= source.Length || source[i + 1] == ')')
                    {
                        data[num_values - 1] = double.Parse(value);
                    }
                }

                double answer = 0.0;
                for (int i = 0; i < data.Length; ++i)
                    answer += data[i];
                return "" + (answer / data.Length);
            }

            //Square Root
            if (source.IndexOf("sqrt(") > -1)
            {
                int sqrt_index = source.IndexOf("sqrt(") + 5;

                if (sqrt_index >= source.Length)
                    return "ERROR";
                if (source.IndexOf(")") < 0)
                    return "" + Math.Sqrt(double.Parse(source.Substring(sqrt_index)));
                else
                    return "" + Math.Sqrt(double.Parse(source.Substring(sqrt_index, source.Length - sqrt_index - 1)));
            }

            //Cosine
            if (source.IndexOf("cos(") > -1)
            {
                int cos_index = source.IndexOf("cos(") + 4;

                if (cos_index >= source.Length)
                    return "ERROR";
                if (source.IndexOf(")") < 0)
                {
                    if (radians)
                        return "" + Math.Cos(double.Parse(source.Substring(cos_index)));
                    else
                        return "" + Math.Cos(double.Parse(source.Substring(cos_index)) * Math.PI / 180.0);
                }
                else
                {
                    if (radians)
                        return "" + Math.Cos(double.Parse(source.Substring(cos_index, source.Length - cos_index - 1)));
                    else
                        return "" + Math.Cos(double.Parse(source.Substring(cos_index, source.Length - cos_index - 1)) * Math.PI / 180.0);
                }
            }

            //Multiplication & Division
            for (int i = 0; i < source.Length; ++i)
            {
                if (source[i] == '*')
                    return "" + (double.Parse(source.Substring(0, i)) * (double.Parse(source.Substring(i + 1))));
                if (source[i] == '/')
                    return "" + (double.Parse(source.Substring(0, i)) / (double.Parse(source.Substring(i + 1))));
                if (source[i] == '+')
                    return "" + (double.Parse(source.Substring(0, i)) + (double.Parse(source.Substring(i + 1))));
                if (source[i] == '-')
                    return "" + (double.Parse(source.Substring(0, i)) - (double.Parse(source.Substring(i + 1))));

            }

            return source;
        }

        private void Calculate()
        {
            if (!init || Input_Textbox.Text == "" || Input_Textbox.Text == "0")
                return;
            init = false;
            string input = Input_Textbox.Text;
            Input_Textbox.Text = "0";
            Output_Textbox.Text = OrderOfOperations(input);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Welcome_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) //Degrees
        {
            radians = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) //Radians
        {
            radians = true;
        }

        private void button10_Click(object sender, EventArgs e) // .
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + ".";
            else
            {
                Input_Textbox.Text = ".";
                init = true;
            }
        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button_0_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "0";
            else
            {
                Input_Textbox.Text = "0";
                init = true;
            }
        }

        private void Button_1_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "1";
            else
            {
                Input_Textbox.Text = "1";
                init = true;
            }
        }

        private void Button_2_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "2";
            else
            {
                Input_Textbox.Text = "2";
                init = true;
            }
        }

        private void Button_3_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "3";
            else
            {
                Input_Textbox.Text = "3";
                init = true;
            }
        }

        private void Button_4_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "4";
            else
            {
                Input_Textbox.Text = "4";
                init = true;
            }
        }

        private void Button_5_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "5";
            else
            {
                Input_Textbox.Text = "5";
                init = true;
            }
        }

        private void Button_6_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "6";
            else
            {
                Input_Textbox.Text = "6";
                init = true;
            }
        }

        private void button1_Click(object sender, EventArgs e) // Button 7
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "7";
            else
            {
                Input_Textbox.Text = "7";
                init = true;
            }
        }

        private void Button_8_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "8";
            else
            {
                Input_Textbox.Text = "8";
                init = true;
            }
        }

        private void Button_9_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "9";
            else
            {
                Input_Textbox.Text = "9";
                init = true;
            }
        }

        private void Layout_TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ZeroPoint_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RadianDegree_Container_Enter(object sender, EventArgs e)
        {

        }

        private void RadianDegree_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Trig_Functions_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_natLog_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "ln(";
            else
            {
                Input_Textbox.Text = "ln(";
                init = true;
            }
        }

        private void button_log_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "log(";
            else
            {
                Input_Textbox.Text = "log(";
                init = true;
            }
        }

        private void button_tanh_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "tanh(";
            else
            {
                Input_Textbox.Text = "tanh(";
                init = true;
            }
        }

        private void button_cosh_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "cosh(";
            else
            {
                Input_Textbox.Text = "cosh(";
                init = true;
            }
        }

        private void button_sinh_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "sinh(";
            else
            {
                Input_Textbox.Text = "sinh(";
                init = true;
            }
        }

        private void button_tan_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "tan(";
            else
            {
                Input_Textbox.Text = "tan(";
                init = true;
            }
        }

        private void button_cos_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "cos(";
            else
            {
                Input_Textbox.Text = "cos(";
                init = true;
            }
        }

        private void button_sin_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "sin(";
            else
            {
                Input_Textbox.Text = "sin(";
                init = true;
            }
        }

        private void Advanced_Container_Enter(object sender, EventArgs e)
        {

        }

        private void Algebraic_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_factorial_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "!";
            else
            {
                Input_Textbox.Text = "!";
                init = true;
            }
        }

        private void button_power_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "^(";
            else
            {
                Input_Textbox.Text = "^(";
                init = true;
            }
        }

        private void button_square_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "^(2)";
            else
            {
                Input_Textbox.Text = "^(2)";
                init = true;
            }
        }

        private void button_powerTen_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "*10^(";
            else
            {
                Input_Textbox.Text = "*10^(";
                init = true;
            }
        }

        private void button_inverse_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "^(-1)";
            else
            {
                Input_Textbox.Text = "^(-1)";
                init = true;
            }
        }

        private void button_sqrt_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "sqrt(";
            else
            {
                Input_Textbox.Text = "sqrt(";
                init = true;
            }
        }

        private void Constants_Container_Enter(object sender, EventArgs e)
        {
        }

        private void Constants_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_pi_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "π";
            else
            {
                Input_Textbox.Text = "π";
                init = true;
            }
        }

        private void button_e_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "e";
            else
            {
                Input_Textbox.Text = "e";
                init = true;
            }
        }

        private void Operator_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_enter_Click(object sender, EventArgs e)
        {
            Calculate();
        }

        private void button_addition_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "+";
            else
            {
                Input_Textbox.Text = "+";
                init = true;
            }
        }

        private void button_subtraction_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "-";
            else
            {
                Input_Textbox.Text = "-";
                init = true;
            }
        }

        private void button_multiplication_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "*";
            else
            {
                Input_Textbox.Text = "*";
                init = true;
            }
        }

        private void button_division_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "/";
            else
            {
                Input_Textbox.Text = "/";
                init = true;
            }
        }

        private void BaseConversion_Container_Enter(object sender, EventArgs e)
        {

        }

        private void BaseConversionGrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BaseSelect_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BaseOption_List_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Button_BaseConvert_Click(object sender, EventArgs e)
        {

        }

        private void UnitConversion_Container_Enter(object sender, EventArgs e)
        {

        }

        private void UnitConversion_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Volume_Container_Enter(object sender, EventArgs e)
        {

        }

        private void VolumeConversion_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void VolumeSelect_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void VolumeFrom_List_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void VolumeTo_List_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //Volume Conversion Button
        {
            double from = double.Parse(Input_Textbox.Text);

            if (VolumeFrom_List.SelectedIndex == 7 && VolumeTo_List.SelectedIndex == 8)
            {
                Output_Textbox.Text = "" + (double.Parse(Input_Textbox.Text) * 1000) + " mL";
                Input_Textbox.Text = "0";
                return;
            }
            if (VolumeFrom_List.SelectedIndex == 8 && VolumeTo_List.SelectedIndex == 7)
            {
                Output_Textbox.Text = "" + (double.Parse(Input_Textbox.Text) / 1000) + " L";
                Input_Textbox.Text = "0";
                return;
            }

            switch (VolumeFrom_List.SelectedIndex) //From
            {
                case 0: //Gallons
                    break;
                case 1: //Quarts
                    from = from / 4;
                    break;
                case 2: //Pints
                    from = from / 8;
                    break;
                case 3: //Cups
                    from = from / 16;
                    break;
                case 4: //Ounces
                    from = from / 128;
                    break;
                case 5: //Tablespoons
                    from = from / 256;
                    break;
                case 6: //Teaspoons
                    from = from / 768;
                    break;
                case 7: //Liters
                    from = from / 3.78541;
                    break;
                case 8: //Milliliters
                    from = from / 3785.41;
                    break;
                default:
                    Output_Textbox.Text = "ERROR";
                    return;
            }

            switch (VolumeTo_List.SelectedIndex) //To
            {
                case 0: //Gallons
                    break;
                case 1: //Quarts
                    Output_Textbox.Text = "" + from * 4 + " qt";
                    break;
                case 2: //Pints
                    Output_Textbox.Text = "" + from * 8 + " pt";
                    break;
                case 3: //Cups
                    Output_Textbox.Text = "" + from * 16 + " cups";
                    break;
                case 4: //Ounces
                    Output_Textbox.Text = "" + from * 128 + " fl oz";
                    break;
                case 5: //Tablespoons
                    Output_Textbox.Text = "" + from * 256 + " tbsp";
                    break;
                case 6: //Teaspoons
                    Output_Textbox.Text = "" + from * 768 + " tsp";
                    break;
                case 7: //Liters
                    Output_Textbox.Text = "" + from * 3.78541 + " L";
                    break;
                case 8: //Milliliters
                    Output_Textbox.Text = "" + from * 3785.41 + " mL";
                    break;
                default:
                    Output_Textbox.Text = "ERROR";
                    return;
            }

            Input_Textbox.Text = "0";
        }

        private void MassWeight_Container_Enter(object sender, EventArgs e)
        {

        }

        private void MassWeightConversion_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MassWeightSelect_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MassWeightFrom_List_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void MassWeightTo_List_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click_1(object sender, EventArgs e) //Mass Weight Conversion Button
        {
            double from = double.Parse(Input_Textbox.Text);


        }

        private void UnitDistance_Container_Enter(object sender, EventArgs e)
        {

        }

        private void DistanceConversion_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DistanceSelect_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DistanceTo_List_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DistanceFrom_List_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Button_DistanceConvert_Click(object sender, EventArgs e)
        {

        }

        private void ConversionTab_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Advanced_Tab_Click(object sender, EventArgs e)
        {

        }

        private void Conversions_Tab_Click(object sender, EventArgs e)
        {

        }

        private void InputOutput_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Clear_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            Input_Textbox.Text = "0";
            Output_Textbox.Text = "0";
            init = false;
        }

        private void Button_ClearEntry_Click(object sender, EventArgs e)
        {
            Input_Textbox.Text = "0";
            init = false;
        }

        private void SimpleCalculator_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ClearFormat_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Parenthesis_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button_CloseParen_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + ")";
            else
            {
                Input_Textbox.Text = ")";
                init = true;
            }
        }

        private void Button_OpenParen_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "(";
            else
            {
                Input_Textbox.Text = "(";
                init = true;
            }
        }

        private void Nonzero_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Button_CopyOutput_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(Output_Textbox.Text);
        }

        private void myLabel_Click(object sender, EventArgs e)
        {

        }

        private void StatisticalFunctions_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button_Mean_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "avg(";
            else
            {
                Input_Textbox.Text = "avg(";
                init = true;
            }
        }

        private void Button_Sum_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "sum(";
            else
            {
                Input_Textbox.Text = "sum(";
                init = true;
            }
        }

        private void Button_StandardDev_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "stddev(";
            else
            {
                Input_Textbox.Text = "stddev(";
                init = true;
            }
        }

        private void Button_Variance_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + "var(";
            else
            {
                Input_Textbox.Text = "var(";
                init = true;
            }
        }

        private void Button_Comma_Click(object sender, EventArgs e)
        {
            if (init)
                Input_Textbox.Text = Input_Textbox.Text + ",";
            else
            {
                Input_Textbox.Text = ",";
                init = true;
            }
        }

        private void StatisticsFunctions_Container_Enter(object sender, EventArgs e)
        {

        }

        private void Statistics_Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void welcomeBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
