﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = TestMob.Models.Contact;

namespace TestMob.Models
{
    public static class ContactRepository
    {
        public static List<Contact> _contacts = new List<Contact>()
        { new Contact {ContactId = 1, Name = "Martins Berzins", Email = "m.berzins09@gmail.com"},
            new Contact {ContactId = 2,Name = "Laila Berzina", Email = "laila.berzina87@gmail.com"}
        };

        public static List<Contact> GetContacts() => _contacts;

        public static Contact GetContactById(int contactId)
        {
            var contact = _contacts.FirstOrDefault(c => c.ContactId == contactId);
            if (contact != null)
            {
                return new Contact
                {
                    ContactId = contact.ContactId,
                    Address = contact.Address,
                    Email = contact.Email,
                    Name = contact.Name,
                    Phone = contact.Phone
                };
            }

            return null;
        }

        public static void UpdateContact(int contactId, Contact contact)
        {
            if (contactId != contact.ContactId) return;

            var contactToUpdate = _contacts.FirstOrDefault(c => c.ContactId == contactId);
            if (contactToUpdate != null)
            {
                    contactToUpdate.Name = contact.Name;
                    contactToUpdate.Email = contact.Email;
                    contactToUpdate.Address = contact.Address;
                    contactToUpdate.Phone = contact.Phone;
            }
        }

        public static void AddContact(Contact contact)
        {
            var maxId = _contacts.Max(x => x.ContactId);
            contact.ContactId = maxId + 1;
            _contacts.Add(contact);
        }

        public static void DeleteContact(int contactId)
        {
            var contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
        }

        public static List<Contact> SearchContacts(string filterText)
        {
            var contacts = _contacts
                .Where(x => !string.IsNullOrWhiteSpace(x.Name) &&
                    x.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts
                    .Where(x => !string.IsNullOrWhiteSpace(x.Email) &&
                                x.Email.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                return contacts;
            }

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts
                    .Where(x => !string.IsNullOrWhiteSpace(x.Phone) && 
                                x.Phone.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                return contacts;
            }

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts
                    .Where(x => !string.IsNullOrWhiteSpace(x.Address) && 
                                x.Address.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                return contacts;
            }

            return contacts;
        }
    }
}
