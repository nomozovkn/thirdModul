﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _3._6_dars;

public class MusicCrudApiBrokker
{
    private HttpClient _httpClient;
    private string _baseUrl;
    public MusicCrudApiBrokker()
    {

        _baseUrl = "http://localhost:7193/api/music"; 
        _httpClient = new HttpClient();
        Add();
        GetAll();
    }

    public void GetAll()
    {
        var url = $"{_baseUrl}/getAllMusic";

        HttpResponseMessage response = _httpClient.GetAsync(url).Result;
        string responseContent = response.Content.ReadAsStringAsync().Result;

        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode == false)
        {
            throw new Exception("response qoniqarli emas");
        }

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.PropertyNameCaseInsensitive = true;

        var music = JsonSerializer.Deserialize<Music[]>(responseContent, options);

        foreach (var m in music)
        {
            Console.WriteLine(m);
        }
    }

    public void Add()
    {
        var url = $"{_baseUrl}/addMusic";
        var music = new Music()
        {
            Name = "Bandaman",
            MB = 4.8,
            AuthorName = "Sherali Jo'rayev",
            Description = "Yaxshi",
            QuentityLikes = 45
        };

        var json = JsonSerializer.Serialize(music);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


        var response = _httpClient.PostAsync(url, content).Result;
        response.EnsureSuccessStatusCode();

        var responseContent = response.Content.ReadAsStringAsync().Result;
        Console.WriteLine(responseContent);

    }
    public void Delete()
    {
        var url = $"{_baseUrl}/deleteMusic";
        var music = new Music()
        {
            Id = Guid.NewGuid()
        };
        var json = JsonSerializer.Serialize(music);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = _httpClient.PostAsync(url, content).Result;
        response.EnsureSuccessStatusCode();
        var responseContent = response.Content.ReadAsStringAsync().Result;
        Console.WriteLine(responseContent);
    }
}
