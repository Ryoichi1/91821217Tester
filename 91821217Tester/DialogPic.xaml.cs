﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _91821217Tester
{
    /// <summary>
    /// Dialog.xaml の相互作用ロジック
    /// </summary>
    public partial class DialogPic
    {
        public enum NAME { JP1_1, JP1_2, PWM_I, PWM_V, その他 }

        NAME testName;
        string PicName = "";

        public DialogPic(string mess, NAME name)
        {
            InitializeComponent();

            this.MouseLeftButtonDown += (sender, e) => this.DragMove();//ウィンドウ全体でドラッグ可能にする

            this.DataContext = State.VmTestStatus;
            State.VmTestStatus.DialogMess = mess;
            testName = name;

            switch (testName)
            {
                case NAME.JP1_1:
                    PicName = "JP1_1.jpg";
                    break;
                case NAME.JP1_2:
                    PicName = "JP1_2.jpg";
                    break;
                case NAME.PWM_I:
                    PicName = "PWM_I.jpg";
                    break;
                case NAME.PWM_V:
                    PicName = "PWM_V.jpg";
                    break;
                case NAME.その他:
                    PicName = "non2.png";
                    break;
            }

        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            Flags.DialogReturn = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Flags.DialogReturn = false;
            this.Close();
        }

        private void metroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            General.PlaySound(General.soundNotice);
            ButtonOk.Focus();
            imagePic.Source = new BitmapImage(new Uri("Resources/Pic/" + PicName, UriKind.Relative));
        }


        bool FlagButtonOkSelected = true;


        private void ButtonOk_GotFocus(object sender, RoutedEventArgs e)
        {
            ButtonOk.Background = General.DialogOnBrush;
            FlagButtonOkSelected = true;
        }

        private void ButtonCancel_GotFocus(object sender, RoutedEventArgs e)
        {
            ButtonCancel.Background = General.DialogOnBrush;
            FlagButtonOkSelected = false;
        }

        private void ButtonOk_LostFocus(object sender, RoutedEventArgs e)
        {
            ButtonOk.Background = Brushes.Transparent;
        }

        private void ButtonCancel_LostFocus(object sender, RoutedEventArgs e)
        {
            ButtonCancel.Background = Brushes.Transparent;
        }

        private void ButtonOk_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!FlagButtonOkSelected)
            {
                ButtonOk.Background = General.DialogOnBrush;
                ButtonCancel.Background = Brushes.Transparent;
            }
        }

        private void ButtonCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (FlagButtonOkSelected)
            {
                ButtonCancel.Background = General.DialogOnBrush;
                ButtonOk.Background = Brushes.Transparent;
            }
        }

        private void ButtonOk_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!FlagButtonOkSelected)
            {
                ButtonOk.Background = Brushes.Transparent;
                ButtonCancel.Background = General.DialogOnBrush;
            }
        }

        private void ButtonCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (FlagButtonOkSelected)
            {
                ButtonCancel.Background = Brushes.Transparent;
                ButtonOk.Background = General.DialogOnBrush;
            }
        }
    }
}
