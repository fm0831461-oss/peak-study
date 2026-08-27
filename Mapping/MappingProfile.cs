using AutoMapper;
using PeekStudy.API.DTOs.UserDTOs;
using PeekStudy.API.DTOs.SubjectDTOs;
using PeekStudy.API.DTOs.StudySourceDTOs;
using PeekStudy.API.DTOs.UnitDTOs;
using PeekStudy.API.DTOs.LessonDTOs;
using PeekStudy.API.DTOs.StudyTask_DTOs;
using PeekStudy.API.DTOs.QuizDTOs;
using PeekStudy.API.DTOs.QuestionDTOs;
using PeekStudy.API.DTOs.OptionDTOs;
using PeekStudy.API.DTOs.QuizAttemptDTOs;
using PeekStudy.API.DTOs.ExamDTOs;
using PeekStudy.API.DTOs.IslandDTOs;
using PeekStudy.API.DTOs.IslandItemDTOs;
using PeekStudy.API.DTOs.FocusSessionDTOs;
using PeekStudy.API.Models;

namespace PeekStudy.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserDTO>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            // Subject
            CreateMap<Subject, SubjectDto>();
            CreateMap<CreateSubjectDto, Subject>();
            CreateMap<UpdateSubjectDto, Subject>();

            // StudySource
            CreateMap<StudySource, StudySourceDto>();
            CreateMap<CreateStudySourceDto, StudySource>();
            CreateMap<UpdateStudySourceDto, StudySource>();

            // COPILOT CHANGE: Added mapping for StudySourceFile -> StudySourceFileDto.
            CreateMap<StudySourceFile, StudySourceFileDto>();

            // Unit
            CreateMap<Unit, UnitDto>();
            CreateMap<CreateUnitDto, Unit>();
            CreateMap<UpdateUnitDto, Unit>();

            // Lesson
            CreateMap<Lesson, LessonDto>();
            CreateMap<CreateLessonDto, Lesson>();
            CreateMap<UpdateLessonDto, Lesson>();

            // StudyTask
            CreateMap<StudyTask, StudyTask_Dto>();
            CreateMap<CreateStudyTask_Dto, StudyTask>();
            CreateMap<UpdateStudyTask_Dto, StudyTask>();

            // Quiz
            CreateMap<Quiz, QuizDto>();

            // Question
            CreateMap<Question, QuestionDto>();

            // Option
            CreateMap<Option, OptionDto>();

            // QuizAttempt
            CreateMap<QuizAttempt, QuizAttemptDto>();

            // Exam
            CreateMap<Exam, ExamDto>();

            // Island
            CreateMap<Island, IslandDto>();

            // IslandItem
            CreateMap<IslandItem, IslandItemDto>();

            // FocusSession
            CreateMap<FocusSession, FocusSessionDto>();
        }
    }
}