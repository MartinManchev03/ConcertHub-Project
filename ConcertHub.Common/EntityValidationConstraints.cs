namespace ConcertHub.Common
{
    public static class EntityValidationConstraints
    {
        public static class ConcertValidation
        {
            public static int ConcertNameMaxLength = 100;
            public static int ConcertNameMinLength = 3;
            public static int DescriptionMaxLength = 500;
            public static int DescriptionMinLength = 10;
            public static int LocationMaxLength = 100;
            public static int LocationMinLength = 5;

        }

        public static class PerformerValidation
        {
            public static int PerformerNameMaxLength = 100;
            public static int PerformerNameMinLength = 3;
            public static int BioMaxLength = 500;
            public static int BioMinLength = 10;
        }
    }
}
