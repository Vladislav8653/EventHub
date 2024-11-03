using Contracts;
using Newtonsoft.Json;

namespace EventHub.InitialData;

public class InitialData
{
    private IRepositoryManager _repositoryManager;
    public InitialData(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public string InitializeDataBase()
    {
        var initialJsonPath = String.Concat(Directory.GetCurrentDirectory(), "initial_data.json");
        var initialJson = File.ReadAllText(initialJsonPath);
        var initialData = JsonConvert.DeserializeObject<Structure>(initialJson);
        if (initialData == null)
            return "Deserializing failed.";
        var categories = initialData.Categories;
        var events = initialData.Events;
        var eventParticipants = initialData.EventParticipants;
        var participants = initialData.Participants;
        foreach (var category in categories)
            _repositoryManager.CategoryRepository.CreateCategory(category);
        foreach (var eventEvent in events)
            _repositoryManager.EventRepository.CreateEvent(eventEvent);
        foreach (var eventParticipant in eventParticipants)
            _repositoryManager.CategoryRepository.CreateCategory(eventParticipant);
        foreach (var participant in participants)
            _repositoryManager.CategoryRepository.CreateCategory(participant);
        
        
    }
    
}