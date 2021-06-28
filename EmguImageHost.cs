using Emgu.CV;
using Emgu.CV.UI;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using static Emgu.CV.UI.ImageBox;

namespace EMGUCVTest
{
    public class EmguImageBoxHost : WindowsFormsHost
    {
        private ImageBox ImageBox { get; } = new ImageBox();

        public EmguImageBoxHost()
        {
            // Установка дочернего элемента
            Child = ImageBox;
            // Запрет на смену Child
            ChildChanged += (s, e) => throw new Exception("Запрещенно изменять дочерний элемент.");

            // Установка начальных значений, согласно заданным в DP-свойствах.
            ImageBox.FunctionalMode = privateFunctionalMode = FunctionalMode;
            ImageBox.SizeMode = privateSizeMode = SizeMode;

            // Подклюсение обработчиков изменения свойств.
            // Возможно это излишне.
            // Но является защитой от изменения свойств ImageBox
            // минуя DP-свойства.
            ImageBox.OnFunctionalModeChanged += OnFunctionalModeChanged;
            ImageBox.SizeModeChanged += OnSizeModeChanged;
            /* 
             * Не знаю какое событие происходит при смене 
             * ImageBox.ImageChanged += OnImageChanged;
            */


        }

        #region Свойство FunctionalMode
        // Если свойство изменилось и оно не равно текущему значению DP-свойства,
        // то DP-свойству присваивается новое значение.
        private void OnFunctionalModeChanged(object sender, EventArgs e)
        {
            if (!Equals(privateFunctionalMode, ImageBox.FunctionalMode))
                FunctionalMode = ImageBox.FunctionalMode;
        }



        /// <summary>Получить или установить функциональный режим для ImageBox.</summary>
        public FunctionalModeOption FunctionalMode
        {
            get { return (FunctionalModeOption)GetValue(FunctionalModeProperty); }
            set { SetValue(FunctionalModeProperty, value); }
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="FunctionalMode"/>.</summary>
        public static readonly DependencyProperty FunctionalModeProperty =
            DependencyProperty.Register(nameof(FunctionalMode), typeof(FunctionalModeOption), typeof(EmguImageBoxHost),
                new PropertyMetadata(FunctionalModeOption.Everything, FunctionalModeChanged));

        // Локальное значение DP-свойства. Это не обязательно,
        // но ускоряет получение значения DP-свойства
        private FunctionalModeOption privateFunctionalMode;

        // Обработка изменения значения DP-свойства.
        private static void FunctionalModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EmguImageBoxHost host = (EmguImageBoxHost)d;
            FunctionalModeOption mode = (FunctionalModeOption)e.NewValue;
            host.privateFunctionalMode = mode;
            host.ImageBox.FunctionalMode = mode;
        }

        #endregion

        #region Свойство SizeMode
        private void OnSizeModeChanged(object sender, EventArgs e)
        {
            if (!Equals(privateSizeMode, ImageBox.SizeMode))
                SizeMode = ImageBox.SizeMode;
        }

        /// <summary>Указывает, как отображается изображение.</summary>
        public PictureBoxSizeMode SizeMode
        {
            get { return (PictureBoxSizeMode)GetValue(SizeModeProperty); }
            set { SetValue(SizeModeProperty, value); }
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="SizeMode"/>.</summary>
        public static readonly DependencyProperty SizeModeProperty =
            DependencyProperty.Register(nameof(SizeMode), typeof(PictureBoxSizeMode), typeof(EmguImageBoxHost),
                new PropertyMetadata(PictureBoxSizeMode.AutoSize, SizeModeChanged));

        private PictureBoxSizeMode privateSizeMode;
        private static void SizeModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EmguImageBoxHost host = (EmguImageBoxHost)d;
            PictureBoxSizeMode mode = (PictureBoxSizeMode)e.NewValue;
            host.privateSizeMode = mode;
            host.ImageBox.SizeMode = mode;
        }
        #endregion

        #region Свойство SizeMode

        /// <summary>
        /// Для чего это свойство
        /// </summary>
        public IInputArray Image
        {
            get { return (IInputArray)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="Image"/>.</summary>
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register(nameof(Image), typeof(IInputArray), typeof(EmguImageBoxHost),
                new PropertyMetadata(null, ImageChanged));


        private IInputArray privateImage;
        private static void ImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EmguImageBoxHost host = (EmguImageBoxHost)d;
            IInputArray image = (IInputArray)e.NewValue;
            host.privateImage= image;
            host.ImageBox.Image= image;
        }
        #endregion

    }
}
