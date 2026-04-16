namespace Services
{
    public class PasswordService : IPasswordService
    {
        public int GetStrengthByPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            int strength = result.Score;
            return strength;
        }
    }
}