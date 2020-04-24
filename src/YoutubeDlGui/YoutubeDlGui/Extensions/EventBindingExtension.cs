namespace YoutubeDlGui.Extensions
{
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;

    /// <summary>
    /// Defines the <see cref="EventBindingExtension" />.
    /// </summary>
    [MarkupExtensionReturnType(typeof(Delegate))]
    public class EventBindingExtension : MarkupExtension
    {
        #region Fields

        /// <summary>
        /// The command binder attached property.
        /// </summary>
        private static readonly DependencyProperty CommandBinderProperty =
            DependencyProperty.RegisterAttached(nameof(CommandBinderProperty).Name(), typeof(ICommand), typeof(EventBindingExtension), new PropertyMetadata(null));

        /// <summary>
        /// The command parameter binder attached property.
        /// </summary>
        private static readonly DependencyProperty CommandParameterBinderProperty =
            DependencyProperty.RegisterAttached(nameof(CommandParameterBinderProperty).Name(), typeof(object), typeof(EventBindingExtension), new PropertyMetadata(null));

        /// <summary>
        /// Name of the Command to be invoked when the event fires
        /// </summary>
        private readonly string _commandName;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBindingExtension"/> class.
        /// </summary>
        public EventBindingExtension()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventBindingExtension"/> class.
        /// </summary>
        /// <param name="command">The command<see cref="string"/>.</param>
        public EventBindingExtension(string command)
        {
            this._commandName = command;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Command.
        /// </summary>
        public Binding Command { get; set; }

        /// <summary>
        /// Gets or sets the CommandParameter.
        /// </summary>
        public Binding CommandParameter { get; set; }

        /// <summary>
        /// Gets or sets the Element.
        /// </summary>
        private DependencyObject Element { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The GetCommandBinder.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="ICommand"/>.</returns>
        public static ICommand GetCommandBinder(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandBinderProperty);
        }

        /// <summary>
        /// The GetCommandParameterBinder.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public static object GetCommandParameterBinder(DependencyObject obj)
        {
            return obj.GetValue(CommandParameterBinderProperty);
        }

        /// <summary>
        /// The SetCommandBinder.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="ICommand"/>.</param>
        public static void SetCommandBinder(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandBinderProperty, value);
        }

        /// <summary>
        /// The SetCommandParameterBinder.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="object"/>.</param>
        public static void SetCommandParameterBinder(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterBinderProperty, value);
        }

        /// <summary>
        /// The ProvideValue.
        /// </summary>
        /// <param name="serviceProvider">.</param>
        /// <returns>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // Retrieve a reference to the InvokeCommand helper method declared below, using reflection
            MethodInfo invokeCommand = GetType().GetMethod(nameof(InvokeCommand), BindingFlags.Instance | BindingFlags.NonPublic);

            if (invokeCommand != null)
            {
                // Check if the current context is an event or a method call with two parameters
                var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
                if (target != null)
                {
                    this.Element = target.TargetObject as DependencyObject;
                    var property = target.TargetProperty;
                    if (property is EventInfo)
                    {
                        // If the context is an event, simply return the helper method as delegate
                        // (this delegate will be invoked when the event fires)
                        var eventHandlerType = (property as EventInfo).EventHandlerType;
                        return invokeCommand.CreateDelegate(eventHandlerType, this);
                    }
                    else if (property is MethodInfo)
                    {
                        // Some events are represented as method calls with 2 parameters:
                        // The first parameter is the control that acts as the event's sender,
                        // the second parameter is the actual event handler
                        var methodParameters = (property as MethodInfo).GetParameters();
                        if (methodParameters.Length == 2)
                        {
                            var eventHandlerType = methodParameters[1].ParameterType;
                            return invokeCommand.CreateDelegate(eventHandlerType, this);
                        }
                    }
                }
            }

            throw new InvalidOperationException("The EventBinding markup extension is valid only in the context of events.");
        }

        /// <summary>
        /// The InvokeCommand.
        /// </summary>
        /// <param name="sender">.</param>
        /// <param name="args">.</param>
        private void InvokeCommand(object sender, EventArgs args)
        {
            BindingOperations.SetBinding(this.Element, CommandParameterBinderProperty, this.CommandParameter);
            var commandParameter = this.Element.GetValue(CommandParameterBinderProperty);
            BindingOperations.ClearBinding(this.Element, CommandParameterBinderProperty);

            if (!string.IsNullOrEmpty(_commandName) && (sender is FrameworkElement control))
            {
                // Find control's ViewModel
                var viewmodel = control.DataContext;
                if (viewmodel != null)
                {
                    // Command must be declared as public property within ViewModel
                    var commandProperty = viewmodel.GetType().GetProperty(_commandName);
                    if (commandProperty != null && commandProperty.GetValue(viewmodel) is ICommand constructorCommand && constructorCommand.CanExecute(args))
                    {
                        // Execute Command and pass event arguments as parameter
                        constructorCommand.Execute(commandParameter ?? args);
                        return;
                    }
                }
            }

            // Get the binding values.
            BindingOperations.SetBinding(this.Element, CommandBinderProperty, this.Command);
            var command = this.Element.GetValue(CommandBinderProperty);
            BindingOperations.ClearBinding(this.Element, CommandBinderProperty);

            // Execute the command.
            if ((command is ICommand relayCommand) && relayCommand.CanExecute(commandParameter))
            {
                relayCommand.Execute(commandParameter);
            }
        }

        #endregion Methods
    }
}