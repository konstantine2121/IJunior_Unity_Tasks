namespace Platformer2D_Task
{
    interface ICollectable
    {
        CollectableTypes CollectableType { get; }

        int NumberOfObjects { get; }

        /// <summary>
        /// Подобрать/убрать объект со сцены.
        /// </summary>
        void Take();
    }
}
