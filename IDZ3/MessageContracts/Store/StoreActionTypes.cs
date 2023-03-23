namespace IDZ3.MessageContracts.Store
{
    /// <summary>
    /// Возможные действия агента склада
    /// </summary>
    public enum StoreActionTypes
    {
        /// <summary>
        /// Проверить наличие продукта
        /// </summary>
        CHECK_PRODUCT = 0,

        /// <summary>
        /// Резервирует продукт
        /// </summary>
        RESERVE_PRODUCT = 1,

        /// <summary>
        /// Действие при готовности блюда
        /// </summary>
        DISH_READY = 2,

        /// <summary>
        /// Отменить резервирование
        /// </summary>
        CANCEL_PRODUCT = 3
    }
}
