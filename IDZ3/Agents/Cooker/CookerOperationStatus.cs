namespace IDZ3.Agents.Cooker
{
    public enum CookerOperationStatus {

        /// <summary>
        /// Начал выполнение операции
        /// </summary>
        StartedTheOperation = 0,

        /// <summary>
        /// Выполнил операцию
        /// </summary>
        PerfomedTheOperation = 1,
        
        /// <summary>
        /// Отменил выполнение операции
        /// </summary>
        CancellTheOperationPerfoming = 2,

        /// <summary>
        /// Оперция остановлена высшим начальством
        /// </summary>
        OperationStopped = 3
    }
}
