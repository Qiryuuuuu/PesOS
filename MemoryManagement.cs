using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PesOS
{
    public class MemoryManager
    {
        private byte[] memoryPool;
        private List<MemoryBlock> allocatedBlocks;

        public MemoryManager(int poolSize)
        {
            memoryPool = new byte[poolSize];
            allocatedBlocks = new List<MemoryBlock>();
        }

        public int Allocate(int size)
        {
            // Find a free block in the memory pool
            int index = FindFreeBlock(size);

            // Allocate memory if a suitable block is found
            if (index != -1)
            {
                MemoryBlock block = new MemoryBlock(index, size);
                allocatedBlocks.Add(block);
                Console.WriteLine($"Allocated {size} bytes at address {index}.");
                return index;
            }
            else
            {
                Console.WriteLine("Memory allocation failed. Not enough free space.");
                return -1;
            }
        }

        public void Free(int address)
        {
            // Find and remove the block associated with the given address
            MemoryBlock block = allocatedBlocks.Find(b => b.Address == address);
            if (block != null)
            {
                allocatedBlocks.Remove(block);
                Console.WriteLine($"Freed memory at address {address}.");
            }
            else
            {
                Console.WriteLine($"Memory at address {address} not found.");
            }
        }

        private int FindFreeBlock(int size)
        {
            // Simple first-fit allocation strategy
            for (int i = 0; i < memoryPool.Length - size; i++)
            {
                if (!allocatedBlocks.Any(b => b.Contains(i, size)))
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public class MemoryBlock
    {
        public int Address { get; private set; }
        public int Size { get; private set; }

        public MemoryBlock(int address, int size)
        {
            Address = address;
            Size = size;
        }

        public bool Contains(int address, int size)
        {
            return address >= Address && address + size <= Address + Size;
        }
    }
}
