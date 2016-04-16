﻿/// <summary>
/// 
/// </summary>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;

namespace Calculator
{
	public partial class MainForm : Form
	{
		private bool needsReset = false;
		private double lastAnswer = 0;
		
		public MainForm()
		{
			InitializeComponent();
			//textBox.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right;
			textBox.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right;
		}
		
		void Trig_MouseClick(object sender, MouseEventArgs e)
		{
			if(needsReset)
			{
				textBox.Clear();
				needsReset = false;
			}
			
			Control ctrl = (Control)sender;
			
			textBox.AppendText(ctrl.Text + "(");
		}
		
		void Number_MouseClick(object sender, MouseEventArgs e)
		{
			if(needsReset)
			{
				textBox.Clear();
				needsReset = false;
			}
			
			Control ctrl = (Control)sender;
			
			textBox.AppendText(ctrl.Text);
		}
		
		void Logarithm_MouseClick(object sender, MouseEventArgs e)
		{
			if(needsReset)
			{
				textBox.Clear();
				needsReset = false;
			}
			
			Control ctrl = (Control)sender;
			
			textBox.AppendText(ctrl.Text + "(");
		}
		
		void Operator_MouseClick(object sender, MouseEventArgs e)
		{
			Control ctrl = (Control)sender;
			
			if(needsReset)
			{
				string answer = textBox.Text.Substring(textBox.Text.IndexOf("\n"));
				textBox.Clear();
				needsReset = false;
				textBox.AppendText("Ans");
			}
			
			switch(ctrl.Text)
			{
				case "/":
					switch(textBox.Text.Substring(textBox.Text.Length - 1))
					{
						case "/":
							return;
						case "*":
							return;
						case "-":
							return;
						case "+":
							return;
					}
					break;
				case "*":
					switch(textBox.Text.Substring(textBox.Text.Length - 1))
					{
						case "/":
							return;
						case "*":
							return;
						case "-":
							return;
						case "+":
							return;
					}
					break;
				case "-":
					switch(textBox.Text.Substring(textBox.Text.Length - 1))
					{
						case "/":
							return;
						case "*":
							return;
					}
					break;
				case "+":
					switch(textBox.Text.Substring(textBox.Text.Length - 1))
					{
						case "/":
							return;
						case "*":
							return;
						case "-":
							return;
						case "+":
							return;
					}
					break;
			}
			
			textBox.AppendText(ctrl.Text);
		}
		
		void Conversion_MouseClick(object sender, MouseEventArgs e)
		{
			Control ctrl = (Control)sender;
			
			if(needsReset)
			{
				textBox.Clear();
				needsReset = false;
				
				textBox.AppendText(ctrl.Text + "(Ans");
				return;
			}
			
			textBox.AppendText(ctrl.Text + "(");
		}
		
		void Parenthesis_MouseClick(object sender, MouseEventArgs e)
		{
			if(needsReset)
			{
				textBox.Clear();
				needsReset = false;
			}
			
			Control ctrl = (Control)sender;
			
			textBox.AppendText(ctrl.Text);
		}
		
		void Button_ansMouseClick(object sender, MouseEventArgs e)
		{
			if(needsReset)
			{
				textBox.Clear();
				needsReset = false;
			}
			
			textBox.AppendText("Ans");
		}
		
		void Button_caretMouseClick(object sender, MouseEventArgs e)
		{
			if(needsReset)
			{
				string answer = textBox.Text.Substring(textBox.Text.IndexOf("\n"));
				textBox.Clear();
				needsReset = false;
				textBox.AppendText("Ans");
			}
			
			textBox.AppendText("^");
		}
		
		void Button_clearMouseClick(object sender, MouseEventArgs e)
		{
			textBox.Clear();
			needsReset = false;
		}
		
		void Button_decimalMouseClick(object sender, MouseEventArgs e)
		{
			if(needsReset)
			{
				textBox.Clear();
				needsReset = false;
			}
			
			if(!Regex.IsMatch(textBox.Text, "[0-9]"))
			{
				textBox.AppendText("0");
			}
			
			textBox.AppendText(".");
		}
		
		void Button_delMouseClick(object sender, MouseEventArgs e)
		{
			if(needsReset)
			{
				textBox.Clear();
				needsReset = false;
				return;
			}
			
			int delete = 1;
			
			if(textBox.Text.Length >= 2)
			{
				switch(textBox.Text.Substring(textBox.Text.Length - 2))
				{
					case "pi":
						delete = 2;
						break;
				}
			}
			
			if(textBox.Text.Length >= 3)
			{
				switch(textBox.Text.Substring(textBox.Text.Length - 3))
				{
					case "ln(":
						delete = 3;
						break;
				}
			}
			
			if(textBox.Text.Length >= 4)
			{
				switch(textBox.Text.Substring(textBox.Text.Length - 4))
				{
					case "sin(":
						delete = 4;
						break;
					case "cos(":
						delete = 4;
						break;
					case "tan(":
						delete = 4;
						break;
					case "log(":
						delete = 4;
						break;
				}
			}
			
			if(textBox.Text.Length >= 5)
			{
				switch(textBox.Text.Substring(textBox.Text.Length - 5))
				{
					case "asin(":
						delete = 5;
						break;
					case "acos(":
						delete = 5;
						break;
					case "atan(":
						delete = 5;
						break;
					case "sinh(":
						delete = 5;
						break;
					case "cosh(":
						delete = 5;
						break;
					case "tanh(":
						delete = 5;
						break;
					case "sqrt(":
						delete = 5;
						break;
				}
			}
			
			if(textBox.Text.Length >= delete)
			{
				textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - delete);
			}
		}
		
		void Button_equalsMouseClick(object sender, MouseEventArgs e)
		{
			string command = textBox.Text;
			
			command = command.Replace("--", "+");
			command = command.Replace("+-", "-");
			
			//Inserts * between number and PI
			command = Regex.Replace(command, "(?<Number>[0-9" + Regex.Escape(".") + "]+)" + "PI", "${Number}*PI");
			
			//Inserts * between PI and number
			command = Regex.Replace(command, "PI" + "(?<Number>[0-9" + Regex.Escape(".") + "]+)", "PI*${Number}");
			
			//Only for when two negatives create a plus behind another plus
			command = command.Replace("++", "+");
			
			//Makes sure integer division is not possible
			command = command.Replace("/", "*1.0/");
			
			//Inserts * between number and Ans
			command = Regex.Replace(command, "(?<Number>[0-9" + Regex.Escape(".") + "]+)" + "Ans", "${Number}*Ans");
			
			//Inserts * between Ans and number
			command = Regex.Replace(command, "Ans" + "(?<Number>[0-9" + Regex.Escape(".") + "]+)", "Ans*${Number}");
			
			command = command.Replace("PI", "Math.PI");
			command = command.Replace("sqrt", "Math.Sqrt");
			command = command.Replace("Ans", lastAnswer.ToString());
			command = command.Replace("asin(", "Math.Asin(");
			command = command.Replace("acos(", "Math.Acos(");
			command = command.Replace("atan(", "Math.Atan(");
			command = command.Replace("sinh(", "Math.Sinh(");
			command = command.Replace("cosh(", "Math.Cosh(");
			command = command.Replace("tanh(", "Math.Tanh(");
			command = command.Replace("ln(", "Math.Log(");
			command = command.Replace("deg(", "180/Math.PI*(");
			command = command.Replace("rad(", "Math.PI/180*(");
			
			//Replace sin( with Math.Sin(
			command = Regex.Replace(command, "^[^A]?sin" + Regex.Escape("("), "Math.Sin(");
			
			//Replace cos( with Math.Cos(
			command = Regex.Replace(command, "^[^A]?cos" + Regex.Escape("("), "Math.Cos(");
			
			//Replace tan( with Math.Tan(
			command = Regex.Replace(command, "^[^A]?tan" + Regex.Escape("("), "Math.Tan(");
			
			//Inserts * between number and (
			command = Regex.Replace(command, "(?<Number>[0-9" + Regex.Escape(".") + "]+)" + Regex.Escape("("), "${Number}*(");
			
			//Inserts * between ( and number
			command = Regex.Replace(command, Regex.Escape(")") + "(?<Number>[0-9" + Regex.Escape(".") + "]+)", ")*${Number}");
			
			command = command.Replace("log(", "Math.Log10(");
			
			//Replaces caret key with Math.Pow
			command = Regex.Replace(command, "(?<Number>[0-9" + Regex.Escape(".") + "]+)" + Regex.Escape("^") +
			                        "(?<Power>[0-9" + Regex.Escape(".") + "]+)", "Math.Pow(${Number}, ${Power})");
			
			//Replaces caret key with parenthesis afterwards with Math.Pow
			command = Regex.Replace(command, "(?<Number>[0-9" + Regex.Escape(".") + "]+)" + Regex.Escape("^") + Regex.Escape("(") +
			                        "(?<Power>.+?)" + Regex.Escape(")"), "Math.Pow(${Number}, ${Power})");
			
			System.Diagnostics.Debug.WriteLine("Expression: " + command);
			
			needsReset = true;
			try
			{
				lastAnswer = ProcessCommand(command);
				textBox.AppendText("\n" + lastAnswer.ToString());
			}
			catch(Exception ex)
			{
				needsReset = false;
				MessageBox.Show(ex.Message, "Invalid Expression!", MessageBoxButtons.OK);
			}
		}
		
		void Button_inverseMouseClick(object sender, MouseEventArgs e)
		{
			if(needsReset)
			{
				textBox.Clear();
				needsReset = false;
				textBox.AppendText("Ans");
			}
			
			textBox.AppendText("^(-1)");
		}
		
		void Button_piMouseClick(object sender, MouseEventArgs e)
		{
			if(needsReset)
			{
				textBox.Clear();
				needsReset = false;
			}
			
			textBox.AppendText("PI");
		}
		
		void Button_sqrtMouseClick(object sender, MouseEventArgs e)
		{
			if(needsReset)
			{
				textBox.Clear();
				needsReset = false;
			}
			
			textBox.AppendText("sqrt(");
		}
		
		void Button_squareMouseClick(object sender, MouseEventArgs e)
		{
			if(needsReset)
			{
				textBox.Clear();
				needsReset = false;
				textBox.AppendText("Ans");
			}
			
			textBox.AppendText("^2");
		}
		
		/// <summary>
		/// A simple function to get the result of a C# expression (basic and advanced expressions possible)
		/// <param name="command">String value containing an expression that can evaluate to a double.</param>
		/// <returns>A double value after evaluating the command string.</returns>
		/// </summary>
		private double ProcessCommand(string command)
		{
			//Create a C# Code Provider
			CSharpCodeProvider myCodeProvider = new CSharpCodeProvider();
			
			//Build the parameters for source compilation
			CompilerParameters cp = new CompilerParameters();
			
			//There is no need to make an EXE file
			cp.GenerateExecutable = false;
			
			//But we do need one in memory
			cp.GenerateInMemory = true;
			
			//This is not necessary, however, if used repeatedly, causes the CLR to not
			//need to load a new assembly each time the function is run
			cp.OutputAssembly = "TempModule";
			//cp.OutputAssembly = Environment.GetEnvironmentVariable("Temp") + "/TempModule";
			
			//The below string is basically the shell of a C# program, that does nothing but
			//contains an Evaluate() method for our purposes (although it does leave the app
			//open to injection attacks
			string TempModuleSource = "namespace ns{" +
				"using System;" +
				"class class1{" +
				"public static double Evaluate(){return " + command + ";}}} "; //Our actual expression evaluator
			
			CompilerResults cr = myCodeProvider.CompileAssemblyFromSource(cp, TempModuleSource);
			
			if(cr.Errors.Count > 0)
			{
				//If a compiler error is generated, we will throw an exception because the
				//syntax was wrong (this is left up to the implementor to verify the calling
				//function). The calling code could trap this in a try loop, and notify a
				//user the command was not understood, for example.
				
				CompilerErrorCollection  compilerErrorCollection = cr.Errors;
				for (int a = 0; a <= compilerErrorCollection.Count - 1; a++)
				{
					System.Diagnostics.Debug.WriteLine(compilerErrorCollection[a].ToString());
				}
				throw new Exception(compilerErrorCollection[0].ErrorText);
			}
			else
			{
				MethodInfo Methinfo = cr.CompiledAssembly.GetType("ns.class1").GetMethod("Evaluate");
				return (double)Methinfo.Invoke(null, null);
			}
		}
	}
}
