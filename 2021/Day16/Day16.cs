public class Day16 : ISolution
{    
    record header(int version, int type);
    abstract record packet(header head)
    {
        public abstract long eval();
    }
    record literal(header head, long value) : packet(head)
    {
        public override long eval() => value;
    }
    record operation(header head, packet[] subpackets) : packet(head)
    {
        public override long eval() => head.type switch
        {
            0 => subpackets.Sum(p => p.eval()),
            1 => subpackets.Aggregate(1L, (product, val) => product * val.eval()),
            2 => subpackets.Min(p => p.eval()),
            3 => subpackets.Max(p => p.eval()),
            5 => subpackets[0].eval() > subpackets[1].eval() ? 1 : 0,
            6 => subpackets[0].eval() < subpackets[1].eval() ? 1 : 0,
            7 => subpackets[0].eval() == subpackets[1].eval() ? 1 : 0,
            _ => throw new NotImplementedException()
        };
    }

    public (long, long) Run(string[] input)
    {
        long pos = 0, vers = 0;
        var reader = new StringReader(string.Join("", 
            Convert.FromHexString(input[0])
            .Select(b => Convert.ToString(b, 2)
            .PadLeft(8, '0'))));
        
        string readRaw(int length)
        {
            var buffer = new char[length];
            pos += reader.Read(buffer, 0, length);
            return new string(buffer);
        }

        int readInt(int length) => Convert.ToInt32(readRaw(length), 2);
        bool readBit() => readInt(1) == 1;

        header readHeader() => new header(readInt(3), readInt(3));

        long readLiteral()
        {
            bool more;
            var parts = new List<string>();
            do
            {
                more = readBit();
                parts.Add(readRaw(4));
            } while (more);

            return Convert.ToInt64(string.Join("", parts), 2);
        }

        packet readPacket()
        {
            var header = readHeader();
            vers += header.version;

            if (header.type == 4)
                return new literal(header, readLiteral());

            var byPacket = readBit();
            var length = readInt(byPacket ? 11 : 15);
            var packets = new List<packet>();

            if (byPacket)
            {
                packets = Enumerable.Range(0, length).Select(i => readPacket())
                    .ToList();
            }
            else
            {
                var end = pos + length;
                do
                {
                    packets.Add(readPacket());
                } while (pos < end);
            }

            return new operation(header, packets.ToArray());
        }

        var root = readPacket();

        return (vers, root.eval());
    }    
}