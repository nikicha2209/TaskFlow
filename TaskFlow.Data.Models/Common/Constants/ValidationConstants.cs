namespace TaskFlow.Data.Models.Common.Constants;

public static class ValidationConstants
{
    public static class User { public const int NameMaxLength = 50; }
    public static class Team { public const int NameMaxLength = 100; public const int DescriptionMaxLength = 500; }
    public static class Project { public const int NameMaxLength = 100; public const int DescriptionMaxLength = 1000; }
    public static class TaskItem { public const int TitleMaxLength = 150; public const int DescriptionMaxLength = 2000; }
    public static class Subtask { public const int TitleMaxLength = 150; public const int DescriptionMaxLength = 1000; }
    public static class Comment { public const int ContentMaxLength = 1000; }
    public static class Notification { public const int MessageMaxLength = 500; }
    public static class ActivityLog { public const int ActionMaxLength = 100; public const int DetailsMaxLength = 500; }
}
