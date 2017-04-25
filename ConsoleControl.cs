using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


    public delegate void CommandEnteredDelegate(string cmd);

    class ConsoleControl : TextBox {

        private int cursorPos;

        public string header;
        private string lastCmd = "";
        public CommandEnteredDelegate CommandEntered; 

        public ConsoleControl() {
            this.Dock = DockStyle.None;
            this.BackColor = Color.Black;
            this.ForeColor = Color.WhiteSmoke;
            this.FontHeight = 11;
            this.Font = new Font("Consolas", 11.25f);
            this.Multiline = true;
            this.BorderStyle = BorderStyle.None;
            this.Enter += ConsoleControl_Enter;
            this.GotFocus += ConsoleControl_GotFocus;
            this.Click += ConsoleControl_Click;
            this.KeyDown += console_KeyDown;
            this.TextChanged += ConsoleControl_TextChanged;
            header = Environment.UserName + ">";
            cursorPos = SelectionStart;
        }

        private void ConsoleControl_TextChanged(object sender, EventArgs e) {
            cursorPos = SelectionStart;
        }

        private void ConsoleControl_Click(object sender, EventArgs e) {
            this.SelectionStart = cursorPos;
        }

        private void ConsoleControl_GotFocus(object sender, EventArgs e) {
            SelectionStart = cursorPos;
        }

        private void ConsoleControl_Enter(object sender, EventArgs e) {
            SelectionStart = cursorPos;
        }

        public void console_KeyDown(object sender, KeyEventArgs e) {

            switch (e.KeyCode) {

                case Keys.Back:
                    if(SelectionStart == GetFirstCharIndexOfCurrentLine() + header.Length) {
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.Up:
                    Text = Text.Substring(0, Text.Length - (Lines[Lines.Length - 1].Length - header.Length));
                    AppendText(lastCmd);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Down:
                    Text = Text.Substring(0, Text.Length - (Lines[Lines.Length - 1].Length - header.Length));
                    SelectionStart = GetFirstCharIndexFromLine(Lines.Length-1) + Lines[Lines.Length - 1].Length;
                    e.SuppressKeyPress = true;
                    break;
                case Keys.Left:
                    if (SelectionStart == GetFirstCharIndexOfCurrentLine() + header.Length) {
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.Enter:
                    lastCmd = Lines[Lines.Length - 1].Substring(header.Length);
                    CommandEntered(Lines[Lines.Length - 1].Substring(header.Length));
                    AppendText(Environment.NewLine + header);
                    e.SuppressKeyPress = true;
                    break;
            }

        }
    }
