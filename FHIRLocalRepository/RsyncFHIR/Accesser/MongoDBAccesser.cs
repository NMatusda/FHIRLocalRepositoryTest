using MongoDB.Driver;
using RsyncFHIR.Resource;
using RsyncFHIR.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsyncFHIR.Accesser
{
    public class MongoDBAccesser : BaseAccesser
    {
        private Config _config;
        private readonly IMongoCollection<PatientResource> _patientResource;

        public MongoDBAccesser() : base()
        {
            this._config = Config.GetConfig();
            var client = new MongoClient(this._config.MongoConnectionString);
            var db = client.GetDatabase(this._config.MongoDBName);

            this._patientResource = db.GetCollection<PatientResource>(this._config.MongoCollectionName);
        }

        //public override List<BaseResource> Get() => this._patientResource.Find(rs => true).ToList();

        //public override BaseResource Get(string id) => this._resource.Find<BaseResource>(rs => rs.id == id).FirstOrDefault();

        public override BaseResource Create(BaseResource resource)
        {
            switch (resource.GetType().Name)
            {
                case "PatientResource":
                    var patjson = (PatientResource)resource;
                    this._patientResource.InsertOne(patjson);
                    break;
                default:
                    break;
            }
            return resource;
        }

        public override void Update(string id, BaseResource resource)
        {
            switch (resource.GetType().ToString())
            {
                case "PatientResource":
                    var patjson = (PatientResource)resource;
                    this._patientResource.ReplaceOne(rs => rs.id == id, patjson);
                    break;
            }
        }

        public override void Remove(BaseResource resource)
        {
            switch (resource.GetType().ToString())
            {
                case "PatientResource":
                    var patjson = (PatientResource)resource;
                    this._patientResource.DeleteOne(rs => rs.id == patjson.id);
                    break;
            }
        }

        public override List<BaseResource> Get()
        {
            throw new NotImplementedException();
        }

        public override BaseResource Get(string id)
        {
            throw new NotImplementedException();
        }

        public override void Remove(string id)
        {
            throw new NotImplementedException();
        }
    }
}
