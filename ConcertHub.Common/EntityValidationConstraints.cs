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

        public static class TicketValidation
        {
            public static int TicketTypeMaxLength = 50;
            public static int TicketTypeMinLength = 3;
        }

        public static class FeedBackValidation
        {
            public static int RatingMax = 5;
            public static int RatingMin = 1;
            public static int CommentMaxLength = 500;
            public static int CommentMinLength = 10;
        }
        
        public static class CategoryValidation
        {
            public static int NameMaxLength = 50;
            public static int NameMinLength = 3;
        }
    }
}
