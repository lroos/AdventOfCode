public class Day01 : ISolution
{
    public (long, long) Run(string[] input)
    {
        var captcha = input[0];
        int sum = 0, sum2 = 0, len = captcha.Length;
        for (int i = 0; i < len; i++)
        {
            if (captcha[i] == captcha[(i + 1) % len])
            {
                sum += int.Parse(captcha[i].ToString());
            }

            if (captcha[i] == captcha[(i + len / 2) % len])
            {
                sum2 += int.Parse(captcha[i].ToString());
            }
        }

        return (sum, sum2);
    }
}