namespace IDZ3.Services.AgentsMailService
{
    // Блокирующая очередь
    public class BlockingQueue<T>
    {
        // Очередь элементов
        private readonly Queue<T> queue = new Queue<T>();

        // Максимальное количество элементов в очереди
        private readonly int maxSize;

        public BlockingQueue( int maxSize ) 
        { 
            this.maxSize = maxSize; 
        }

        /// <summary>
        /// Добавлет новый элемент в очередь, блокирует поток в случае отсутсвтия места
        /// </summary>
        public void PushItem( T item )
        {
            lock ( queue )
            {
                while ( queue.Count >= maxSize )
                {
                    Monitor.Wait( queue );
                }
                queue.Enqueue( item );
                if ( queue.Count == 1 )
                {
                    Monitor.PulseAll( queue );
                }
            }
        }

        /// <summary>
        /// Выдает элемент очереди, блокирует поток в случае отсутвия элементов
        /// </summary>
        public T PopItem()
        {
            lock ( queue )
            {
                while ( queue.Count == 0 )
                {
                    Monitor.Wait( queue );
                }
                T item = queue.Dequeue();
                if ( queue.Count == maxSize - 1 )
                {
                    Monitor.PulseAll( queue );
                }
                return item;
            }
        }
    }
}
