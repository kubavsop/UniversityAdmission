﻿using Admission.IntegrationEvents.BaseEvents;

namespace Admission.IntegrationEvents.Events.EducationProgram;

public sealed class ProgramNameChangedIntegrationEvent: NameChangedIntegrationEvent<Guid>;