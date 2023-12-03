namespace RakePlugin.Model
{
    /// <summary>
    /// Статический класс, возвращающий названия процессов САПР.
    /// </summary>
    public static class CAD
    {
        /// <summary>
        /// Метод вазврата процесса Компаса.
        /// </summary>
        /// <returns>Процесс.</returns>
        public static string Kompas()
        {
            return "kStudy";
        }

        /// <summary>
        /// Метод возврата процесса Солида.
        /// </summary>
        /// <returns>Процесс.</returns>
        public static string SolidWorks()
        {
            return "SLDWORKS";
        }
    }
}
