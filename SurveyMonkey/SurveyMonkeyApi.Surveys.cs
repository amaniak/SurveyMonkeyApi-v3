﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SurveyMonkey.Containers;
using SurveyMonkey.Helpers;
using SurveyMonkey.RequestSettings;

namespace SurveyMonkey
{
    public partial class SurveyMonkeyApi
    {
        public List<Survey> GetSurveyList()
        {
            var settings = new GetSurveyListSettings();
            return GetSurveyListPager(settings);
        }

        public List<Survey> GetSurveyList(GetSurveyListSettings settings)
        {
            return GetSurveyListPager(settings);
        }

        private List<Survey> GetSurveyListPager(GetSurveyListSettings settings)
        {
            string endpoint = "/surveys";
            return Page(settings, endpoint, typeof(List<Survey>), 1000).ToList().ConvertAll(o => (Survey)o);
        }

        public Survey GetSurveyOverview(long id)
        {
            var verb = Verb.GET;
            var endpoint = String.Format("/surveys/{0}", id);
            JToken result = MakeApiRequest(endpoint, verb, new RequestData());
            var survey = result.ToObject<Survey>();
            return survey;
        }

        public Survey GetSurveyDetails(long id)
        {
            string endPoint = "/surveys/{0}/details";
            var verb = Verb.GET;
            var fullEndpoint = String.Format(endPoint, id);
            JToken result = MakeApiRequest(fullEndpoint, verb, new RequestData());
            var survey = result.ToObject<Survey>();
            return survey;
        }

        public List<SurveyCategory> GetSurveyCategories()
        {
            var settings = new GetSurveyCategoriesSettings();
            return GetSurveyCategoriesPager(settings);
        }

        public List<SurveyCategory> GetSurveyCategories(GetSurveyCategoriesSettings settings)
        {
            return GetSurveyCategoriesPager(settings);
        }

        private List<SurveyCategory> GetSurveyCategoriesPager(GetSurveyCategoriesSettings settings)
        {
            //Get the specific page & quantity
            if (settings.Page.HasValue || settings.PerPage.HasValue)
            {
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                return GetSurveyCategoriesRequest(requestData);
            }

            //Auto-page
            const int maxResultsPerPage = 1000;
            var results = new List<SurveyCategory>();
            bool cont = true;
            int page = 1;
            while (cont)
            {
                settings.Page = page;
                settings.PerPage = maxResultsPerPage;
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                var newResults = GetSurveyCategoriesRequest(requestData);
                if (newResults.Count > 0)
                {
                    results.AddRange(newResults);
                }
                if (newResults.Count < maxResultsPerPage)
                {
                    cont = false;
                }
                page++;
            }
            return results;
        }

        private List<SurveyCategory> GetSurveyCategoriesRequest(RequestData requestData)
        {
            string endPoint = "/survey_categories";
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, requestData);
            var categories = result["data"].ToObject<List<SurveyCategory>>();
            return categories;
        }

        public List<SurveyTemplate> GetSurveyTemplates()
        {
            var settings = new GetSurveyTemplatesSettings();
            return GetSurveyTemplatesPager(settings);
        }

        public List<SurveyTemplate> GetSurveyTemplates(GetSurveyTemplatesSettings settings)
        {
            return GetSurveyTemplatesPager(settings);
        }

        private List<SurveyTemplate> GetSurveyTemplatesPager(GetSurveyTemplatesSettings settings)
        {
            //Get the specific page & quantity
            if (settings.Page.HasValue || settings.PerPage.HasValue)
            {
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                return GetSurveyTemplatesRequest(requestData);
            }

            //Auto-page
            const int maxResultsPerPage = 1000;
            var results = new List<SurveyTemplate>();
            bool cont = true;
            int page = 1;
            while (cont)
            {
                settings.Page = page;
                settings.PerPage = maxResultsPerPage;
                var requestData = RequestSettingsHelper.GetPopulatedProperties(settings);
                var newResults = GetSurveyTemplatesRequest(requestData);
                if (newResults.Count > 0)
                {
                    results.AddRange(newResults);
                }
                if (newResults.Count < maxResultsPerPage)
                {
                    cont = false;
                }
                page++;
            }
            return results;
        }

        private List<SurveyTemplate> GetSurveyTemplatesRequest(RequestData requestData)
        {
            string endPoint = "/survey_templates";
            var verb = Verb.GET;
            JToken result = MakeApiRequest(endPoint, verb, requestData);
            var templates = result["data"].ToObject<List<SurveyTemplate>>();
            return templates;
        }
    }
}