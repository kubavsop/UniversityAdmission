﻿using Admission.IntegrationEvents.BaseEvents;

namespace Admission.IntegrationEvents.Events.Faculty;

public sealed class FacultyNameChangedIntegrationEvent: NameChangedIntegrationEvent<Guid>;