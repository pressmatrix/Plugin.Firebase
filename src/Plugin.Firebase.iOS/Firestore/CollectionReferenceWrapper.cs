﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.CloudFirestore;
using Plugin.Firebase.Abstractions.Common;
using Plugin.Firebase.Abstractions.Firestore;

namespace Plugin.Firebase.iOS.Firestore
{
    public sealed class CollectionReferenceWrapper : ICollectionReference
    {
        private readonly CollectionReference _reference;
        
        public CollectionReferenceWrapper(CollectionReference reference)
        {
            _reference = reference;
        }

        public IDocumentReference GetDocument(string documentPath)
        {
            return new DocumentReferenceWrapper(_reference.GetDocument(documentPath));
        }

        public IDocumentReference CreateDocument()
        {
            return new DocumentReferenceWrapper(_reference.CreateDocument());
        }

        public IQuery WhereEqualsTo(string field, object value)
        {
            return new QueryWrapper(_reference.WhereEqualsTo(field, value));
        }

        public IQuery WhereGreaterThan(string field, object value)
        {
            return new QueryWrapper(_reference.WhereGreaterThan(field, value));
        }

        public IQuery WhereLessThan(string field, object value)
        {
            return new QueryWrapper(_reference.WhereLessThan(field, value));
        }

        public IQuery WhereGreaterThanOrEqualsTo(string field, object value)
        {
            return new QueryWrapper(_reference.WhereGreaterThanOrEqualsTo(field, value));
        }

        public IQuery WhereLessThanOrEqualsTo(string field, object value)
        {
            return new QueryWrapper(_reference.WhereLessThanOrEqualsTo(field, value));
        }

        public IQuery OrderBy(string field)
        {
            return new QueryWrapper(_reference.OrderedBy(field));
        }

        public IQuery StartingAt(object[] fieldValues)
        {
            return new QueryWrapper(_reference.StartingAt(fieldValues));
        }

        public IQuery EndingAt(object[] fieldValues)
        {
            return new QueryWrapper(_reference.EndingAt(fieldValues));
        }
        
        public Task<IDocumentReference> AddDocumentAsync(object data)
        {
            var tcs = new TaskCompletionSource<IDocumentReference>();
            DocumentReference documentReference = null;
            documentReference = _reference.AddDocument(data.ToDictionary(), error => {
                if(error == null) {
                    tcs.SetResult(new DocumentReferenceWrapper(documentReference));
                } else {
                    tcs.SetException(new FirebaseException(error?.LocalizedDescription));
                }
            });
            return tcs.Task;
        }
    }
}