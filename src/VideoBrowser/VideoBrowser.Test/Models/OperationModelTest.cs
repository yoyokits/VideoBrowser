namespace VideoBrowser.Test.Models
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.ObjectModel;
    using VideoBrowser.Models;
    using VideoBrowser.Test.TestHelpers;

    /// <summary>
    /// Defines the <see cref="OperationModelTest" />.
    /// </summary>
    [TestClass]
    public class OperationModelTest
    {
        #region Methods

        /// <summary>
        /// The Test.
        /// </summary>
        [TestMethod]
        public void ContainsTest()
        {
            var operation1 = new DummyOperation
            {
                Output = "NewVideo1.mp4"
            };
            var operation2 = new DummyOperation
            {
                Output = "NewVideo2.mp4"
            };
            var operation3 = new DummyOperation
            {
                Output = "NewVideo1.mp4"
            };

            var model1 = new OperationModel(operation1);
            var model2 = new OperationModel(operation2);
            var model3 = new OperationModel(operation3);
            var models = new ObservableCollection<OperationModel>
            {
                model1,
                model2
            };

            models.Contains(model1).Should().BeTrue();
            models.Contains(model2).Should().BeTrue();
            models.Contains(model3).Should().BeTrue();
        }

        #endregion Methods
    }
}